﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Document.Events {
    //Tab已经添加事件;
    public class DocumentAddedEvent:PubSubEvent<(IDocumentBase tab,IDocumentService owner)> {
    }
}