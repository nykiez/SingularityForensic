using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileExplorer.Models;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer.Events.FolderBrowser {
    /// <summary>
    /// 为文件资源管理器加入导航菜单;
    /// </summary>
    [Export(typeof(IFolderBrowserDataContextCreatedEventHandler))]
    class OnFolderBrowserDataContextCreatedOnNavMenuHandler : IFolderBrowserDataContextCreatedEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle(IFolderBrowserDataContext folderBrowserDataContext) {
            if(folderBrowserDataContext == null) {
                return;
            }
            if(folderBrowserDataContext.FolderBrowserViewModel == null) {
                return;
            }

            var navContext = FileExplorerDataContextFactory.CreateNavMenuDataContext();
            folderBrowserDataContext.StackGrid.AddChild(navContext,Contracts.Controls.GridChildLength.Auto);
            folderBrowserDataContext.FolderBrowserViewModel.IsBusy = true;
            folderBrowserDataContext.FolderBrowserViewModel.BusyWord = LanguageService.FindResourceString(Constants.BusyWord_NavMenuBeingBuilt);
            
            
            ThreadInvoker.BackInvoke(() => {
                //跳转目录时导航节点变化;
                folderBrowserDataContext.FolderBrowserViewModel.CurrentPathChanged += (sender, e) => {
                    var currentPath = folderBrowserDataContext.FolderBrowserViewModel.CurrentPath;
                    if (string.IsNullOrEmpty(currentPath)) {
                        return;
                    }
                    navContext.NavMenuViewModel.SelectedPath = currentPath;
                };

                //导航选中节点发生变化时跳转目录;
                navContext.NavMenuViewModel.InternalSelectedPathChanged += (sender, e) => {
                    var slPath = navContext.NavMenuViewModel.SelectedPath;
                    if (string.IsNullOrEmpty(slPath)) {
                        return;
                    }

                    folderBrowserDataContext.FolderBrowserViewModel.CurrentPath = slPath;
                };

                //创建数节点;
                var node = CreateNodeModelWithFileCollection(folderBrowserDataContext.FolderBrowserViewModel.OwnedFileCollection);
                TraveseTreeChildren(
                    folderBrowserDataContext.FolderBrowserViewModel.OwnedFileCollection,
                    node,
                    CreateChildrenCollection,
                    (pNode, newNode) => pNode.Children.Add(newNode),
                    CreateNodeModelWithFileCollection
                );

                navContext.NavMenuViewModel.RootNavNode = node;
                folderBrowserDataContext.FolderBrowserViewModel.IsBusy = false;
                folderBrowserDataContext.FolderBrowserViewModel.BusyWord = string.Empty;
            });
        }

        /// <summary>
        /// 根据文件创建一个节点;
        /// </summary>
        /// <param name="haveFileCollection"></param>
        /// <returns></returns>
        private static INavNodeModel CreateNodeModelWithFileCollection(IHaveFileCollection haveFileCollection) {
            var node = NavNodeFactory.CreateNew();
            node.Name = haveFileCollection.Name;
            return node;
        }

        private static IEnumerable<IHaveFileCollection> CreateChildrenCollection(IHaveFileCollection haveFileCollection) {
            foreach (var file in haveFileCollection.Children) {
                if (file is IHaveFileCollection cCollection) {
                    if(cCollection is IDirectory direct && (direct.IsBack || direct.IsLocalBackUp)) {
                        continue;
                    }
                    yield return cCollection;
                }
            }
        }

        /// <summary>
        /// 递归遍历树集得到一个单位节点;
        /// </summary>
        private static void TraveseTreeChildren<TEntity,TNode>(
            TEntity entitiy,
            TNode node,
            Func<TEntity,IEnumerable<TEntity>> getChildren,
            Action<TNode,TNode> doWithNode,
            Func<TEntity,TNode> nodeFactory) {

            foreach (var child in getChildren(entitiy)) {
                var newNode = nodeFactory.Invoke(child);
                doWithNode(node, newNode);
                TraveseTreeChildren(child, newNode, getChildren, doWithNode, nodeFactory); 
            }
        }
    }
}
