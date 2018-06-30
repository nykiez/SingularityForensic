using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoUI.Controls {
    /// <summary>
    /// Interaction logic for TestBreadcrumb.xaml
    /// </summary>
    public partial class TestBreadcrumb : UserControl {
        public TestBreadcrumb() {
            InitializeComponent();
            this.DataContext = new TestBreadcrumbViewModel();
        }
        //private void explorerBreadcrumb_CurrentItemChanged(object sender, Telerik.Windows.RadRoutedEventArgs e) {
        //    this.explorerTree.BringPathIntoView(this.explorerBreadcrumb.Path);
        //    this.explorerTree.SelectedItem = this.explorerBreadcrumb.CurrentItem;
        //}

        //private void explorerTree_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        //    if (e.AddedItems.Count > 0 && this.explorerTree.ContainerFromItemRecursive(e.AddedItems[0]) != null) {
        //        this.explorerBreadcrumb.Path = this.explorerTree.ContainerFromItemRecursive(e.AddedItems[0]).FullPath;
        //    }
        //}
    }

    public class TestBreadcrumbViewModel:BindableBase {
        private ObservableCollection<ExplorerItem> items = new ObservableCollection<ExplorerItem>();
        private ExplorerItem root;

        private ExplorerItem _currentItem;
        public ExplorerItem CurrentItem {
            get => _currentItem;
            set {
                _currentItem = value;
                //SetProperty(ref _currentItem, value);
                SetProperty(ref _treeSelectedItem, value,nameof(TreeSelectedItem));
            }
        }


        private ExplorerItem _treeSelectedItem;
        public ExplorerItem TreeSelectedItem {
            get => _treeSelectedItem;
            set  {
                _treeSelectedItem = value;


                //SetProperty(ref _treeSelectedItem, value);
                SetProperty(ref _currentItem, value,nameof(CurrentItem));
            }
        }


#if WPF
        private string prefix = "pack://application:,,,/Breadcrumb;component";
#else
        private readonly string _prefix = string.Empty;
#endif

        public TestBreadcrumbViewModel() {
            this.LoadItems();
        }

        public ExplorerItem Root {
            get {
                return root;
            }
            set {
                root = value;
            }
        }

        public ObservableCollection<ExplorerItem> Items {
            get {
                return this.items;
            }
        }

        private void LoadItems() {
            ExplorerItem getNewItem() {
                //ImageSourceConverter isc = new ImageSourceConverter();

                ExplorerItem personalInfo = new ExplorerItem() {
                    Header = "Personal Folders",
                    //Image = (ImageSource)isc.ConvertFromString(prefix + "/Images/PersonalFolders.png"),
                    Children = new ObservableCollection<ExplorerItem>()
                    {
                    new ExplorerItem() {Header = "Deleted Items(6)",
                        //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/DeletedItems.png")
                    },
                    new ExplorerItem() {Header = "Drafts",
                        //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/Drafts.png")
                    },
                    new ExplorerItem() {Header = "Inbox(14)",
                        //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/Inbox.png"),
                                        Children = new ObservableCollection<ExplorerItem>()
                                        {
                                            new ExplorerItem() {Header = "Folders",
                                                //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/Folder.png")
                                            },
                                        }
                                    },
                    new ExplorerItem() {Header = "Junk E-mails",
                        //Image = (ImageSource)isc.ConvertFromString(prefix + "/Images/junk.png")
                    },
                    new ExplorerItem() {Header = "Outbox",
                        //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/outbox.png")
                    },
                    new ExplorerItem() {Header = "Sent Items",
                        //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/sent.png")
                    },
                    new ExplorerItem() {Header = "Search Folder",// Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/searchFolder.png"),
                                        Children = new ObservableCollection<ExplorerItem>()
                                        {
                                            new ExplorerItem() {Header = "From Follow up",
                                                //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/search.png")
                                            },
                                            new ExplorerItem() {Header = "Large Mail",
                                                // Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/search.png")
                                            },
                                            new ExplorerItem() {Header = "Unread Mail",
                                                //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/search.png")
                                            },
                                        }
                                    }
                },
                };

                ExplorerItem programFiles = new ExplorerItem() {
                    Header = "Program Files",
                    //Image = (ImageSource)isc.ConvertFromString(prefix + "/Images/OpenedFolder.png"),
                    Children = new ObservableCollection<ExplorerItem>()
                    {
                    new ExplorerItem() { Header = "Microsoft",
                        //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/Folder.png")
                    },
                    new ExplorerItem() { Header = "Microsoft.NET",
                        //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/Folder.png")
                    },
                    new ExplorerItem()
                    {
                        Header = "Internet Explorer",
                        //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/OpenedFolder.png"),
                        Children = new ObservableCollection<ExplorerItem>(){
                                                                                new ExplorerItem() {Header = "SIGNUP",
                                                                                    //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/Folder.png")
                                                                                }
                                                                           }
                    }
                },
                };

                ExplorerItem programFiles86 = new ExplorerItem() {
                    Header = "Program Files(86)",
                    //Image = (ImageSource)isc.ConvertFromString(prefix + "/Images/OpenedFolder.png"),
                    Children = new ObservableCollection<ExplorerItem>()
                    {
                    new ExplorerItem() { Header = "Microsoft",
                        //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/Folder.png")
                    },
                    new ExplorerItem() { Header = "Microsoft.NET",
                        //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/Folder.png")
                    },
                    new ExplorerItem()
                    {
                        Header = "Skype",
                        //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/OpenedFolder.png"),
                        Children = new ObservableCollection<ExplorerItem>(){
                                                                                new ExplorerItem() {Header = "Phone",
                                                                                    //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/Folder.png")
                                                                                },
                                                                                new ExplorerItem() {Header = "Toolbars",
                                                                                    //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/Folder.png")
                                                                                },
                                                                                new ExplorerItem() {Header = "Plugin Manager",
                                                                                    //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/Folder.png")
                                                                                }
                                                                           }
                    },
                    new ExplorerItem()
                    {
                        Header = "Notepad++",
                        //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/OpenedFolder.png"),
                        Children = new ObservableCollection<ExplorerItem>(){
                                                                                new ExplorerItem() {Header = "localization",
                                                                                    //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/Folder.png")
                                                                                },
                                                                                new ExplorerItem() {Header = "plugins",
                                                                                    //Image = (ImageSource)isc.ConvertFromString(prefix + "/Images/junk.png")
                                                                                }
                                                                           }
                    }
                },
                };

                ExplorerItem downloads = new ExplorerItem() {
                    Header = "Downloads",
                    // Image = (ImageSource)isc.ConvertFromString(prefix + "/Images/OpenedFolder.png"),
                    Children = new ObservableCollection<ExplorerItem>()
                    {
                    new ExplorerItem() { Header = "Music",
                        //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/Folder.png")
                    },
                    new ExplorerItem() { Header = "Movies",
                        //Image =  (ImageSource)isc.ConvertFromString(prefix + "/Images/Folder.png") 
                    }
                },
                };

                ExplorerItem localHard = new ExplorerItem() {

                    Header = "Local Disk (C:)",
                    //  Image = (ImageSource)isc.ConvertFromString(prefix + "/Images/HardDrive.png"),
                    Children = new ObservableCollection<ExplorerItem>()
                    {
                    personalInfo,
                    programFiles,
                    programFiles86,
                    downloads
                }
                };

                ExplorerItem localHard2 = new ExplorerItem() {

                    Header = "Local Disk (D:)",
                    // Image = (ImageSource)isc.ConvertFromString(prefix + "/Images/HardDrive.png")
                };

                ExplorerItem computer = new ExplorerItem() {

                    Header = "Computer",
                    //  Image = (ImageSource)isc.ConvertFromString(prefix + "/Images/Computer.png"),
                    Children = new ObservableCollection<ExplorerItem>()
                    {
                    localHard,
                    localHard2
                }
                };
                return computer;
            }
            

            this.Root = new ExplorerItem() {
                Header = "Desktop",
                //Image = (ImageSource)isc.ConvertFromString(prefix + "/Images/Desktop.png")
            };
            for (int i = 0; i < 1000; i++) {
                var computer = getNewItem();
                root.Children.Add(computer);
            }
            this.items = new ObservableCollection<ExplorerItem>() { this.Root };
        }
    }
    public class ExplorerItem {
        public string Header { get; set; }
        public string PreviewHeader { get; set; }
        //public ImageSource Image { get; set; }

        private ObservableCollection<ExplorerItem> _children;

        public ObservableCollection<ExplorerItem> Children {
            get {
#if DEBUG
                ChildrenTimes++;
#endif
                return _children;
            }
            set { _children = value; }
        }
        
#if DEBUG
        public static int ChildrenTimes { get; set; } = 0;
#endif
        public ExplorerItem() {
            this.Children = new ObservableCollection<ExplorerItem>();
        }
    }
}
