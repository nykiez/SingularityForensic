﻿using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer {
    public static class FileExtensions {
        public static string GetPartFixAndName(this IPartition part) {
            if(!(part.Parent is IHaveFileCollection haveCollection)) {
                LoggerService.WriteCallerLine($"{nameof(haveCollection)} can't be null.");
                return part.Name;
            }
            
            var partSb = new StringBuilder($"{LanguageService.FindResourceString(Constants.Prefix_Partition)} {haveCollection.IndexOf(part) + 1}");
            if (!string.IsNullOrEmpty(part.Name)) {
                partSb.Append($"({part.Name})");
            }

            return partSb.ToString();
        }
    }
}
