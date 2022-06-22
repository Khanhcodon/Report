function TreeController(a, b) { this._model = a || null; this._view = b || null; this.tmpHiddenNode = [] } TreeController.subTreeId = "treeDiv12"; TreeController.prototype.setModel = function (a) { this._model = a }; TreeController.prototype.setView = function (a) { this._view = a }; TreeController.prototype.init = function () { this.initObserverEvent(); this._view.createTree({}); mailbox.getInstance().loadFromBpr || (this.createDragDrop(), this._view.createContextMenu()) };
TreeController.prototype.initObserverEvent = function () {
    var a = this; this._view.selectFolderEvent.attach(function (b, c) { a.handleSelectFolder(c) }); this._view.postToThisFolderEvent.attach(function () { a.postToThisFolder() }); this._view.createNewFolderEvent.attach(function (b, c) { a.createNewFolder(c) }); this._model.initCalendarEvent.attach(function () { a._view.initCalendar() }); this._model.initContactEvent.attach(function () { a._view.initContact() }); this._model.initFacebookEvent.attach(function () { a._view.initFacebook() });
    this._view.emptyFolderEvent.attach(function (b, c) { a.emptyFolder(c) }); this._view.deleteFolderEvent.attach(function (b, c) { a.deleteFolder(c) }); this._view.markAllReadFolderEvent.attach(function (b, c) { a.markAllReadFolder(c) }); this._view.renameFolderEvent.attach(function (b, c) { a.renameFolder(c) }); this._view.syncFolderEvent.attach(function (b, c) { a.syncFolder(c) }); this._view.manageFoldersEvent.attach(function () { a.manageFolders() }); this._model.redrawTreeViewEvent.attach(function () { a._view.redrawTreeView() }); this._view.createShareFolderEvent.attach(function (b,
    c) { a.createShareFolder(c) }); this._view.createPublicFolderEvent.attach(function (b, c) { a.createPublicFolder(c) }); this._view.editFolderEvent.attach(function (b, c) { a.editFolder(c) }); this._view.moveFolderEvent.attach(function (b, c) { a.moveFolder(c) }); this._view.folderStructureChangeEvent.attach(function (b, c) { a.folderStructureChange(c) })
}; TreeController.getInstance = function () { this._instance || (this._instance = new TreeController); return this._instance };
TreeController.prototype.handleSelectFolder = function (a) { QuickSearch.getInstance().reset(); abortConnect(); this._model.handleSelectFolder(a) }; TreeController.prototype.getRoot = function () { return this._view.getRoot() }; TreeController.prototype.getBmailFolderStructure = function () { return this._model.getBmailFolderStructure() }; TreeController.prototype.getHighlightedNode = function () { return this._view.selectedNode };
TreeController.prototype.getNodeByProperty = function (a, b) { return this._view.getNodeByProperty(a, b) }; TreeController.prototype.getAccountArray = function () { return this._model.getAccountArray() }; TreeController.prototype.isMainFolder = function (a) { return "10" != a && "2" != a && "3" != a && "4" != a && "5" != a && "6" != a ? !1 : !0 }; TreeController.prototype.createDragDrop = function () { (new TreeDragDrop).init() };
TreeController.prototype.createShareFolder = function (a) {
    YAHOO.util.Get.script(["js/shareFolder.js"], {
        onFailure: function () { }, onSuccess: function () {
            var b = a.node, c = Session.getInstance().getUser(), d = Session.getInstance().getAuthen(), c = getAllFolder1(c, d, b.labelElId); c.Body.GetFolderResponse.folder && c.Body.GetFolderResponse.folder[0].acl && c.Body.GetFolderResponse.folder[0].acl.grant ? c.Body.GetFolderResponse.folder[0].acl.grant[0].publicFolder ? new MessageBox(messageBoxMsg, errorNoPermissionMsg) : shareFolder(b.label,
            b.labelElId) : shareFolder(b.label, b.labelElId)
        }
    })
};
TreeController.prototype.createPublicFolder = function (a) {
    YAHOO.util.Get.script(["js/shareFolder.js"], {
        onFailure: function () { }, onSuccess: function () {
            var b = a.node, c = Session.getInstance().getUser(), d = Session.getInstance().getAuthen(), c = getAllFolder1(c, d, b.labelElId); c.Body.GetFolderResponse.folder && c.Body.GetFolderResponse.folder[0].acl && c.Body.GetFolderResponse.folder[0].acl.grant ? c.Body.GetFolderResponse.folder[0].acl.grant[0].publicFolder ? publicFolder(b.label, b.labelElId) : new MessageBox(messageBoxMsg,
            errorNoPermissionMsg) : publicFolder(b.label, b.labelElId)
        }
    })
}; TreeController.prototype.editFolder = function (a) { YAHOO.util.Get.script(["js/shareFolder.js"], { onFailure: function () { }, onSuccess: function () { var b = a.node; editShareFolder(b.label, b.labelElId, a.flag) } }) }; TreeController.prototype.folderStructureChange = function (a) { a = a.rootNode || this._view.getRoot(); this.sendFolderStructure(a) }; TreeController.prototype.addActiveStyle = function (a) { this._view.addActiveStyle(a) };
TreeController.prototype.createDialogFolder = function (a) {
    var b = a.node || null, c = a.dialogId || "moveDialog", d = ""; b && (d = b.labelElId); var f = a.label || "", e = a.dialogHeader || "", g = document.body.appendChild(document.createElement("div")); g.setAttribute("id", c); g.appendChild(document.createElement("div")).className = "hd"; c = g.appendChild(document.createElement("div")); c.className = "bd"; var h = document.createElement("div"); h.setAttribute("id", "divFolder"); if (f) {
        var l = document.createElement("label"); l.innerHTML = f + ": ";
        h.appendChild(l); f = document.createElement("input"); f.setAttribute("type", "text"); f.setAttribute("size", "30"); f.setAttribute("id", "foldertoMove"); f.setAttribute("readonly", "readonly"); h.appendChild(f)
    } f = document.createElement("input"); f.setAttribute("type", "hidden"); f.setAttribute("id", "idSubFolder"); h.appendChild(f); c.appendChild(h); c = c.appendChild(document.createElement("div")); c.setAttribute("id", "bodyMoveDialog"); c.style.overflow = "auto"; c.style.background = "none repeat scroll 0 0 #FFFFFF"; c.style.height =
    "300px"; c.style.width = "438px"; c.style.border = "1px solid #c0c0c0"; c.style.marginTop = "5px"; c.style.bgcolor = "#fdfcfa"; c = c.appendChild(document.createElement("div")); c.id = TreeController.subTreeId + share.INDEX; share.INDEX++; h = new TreeModel(this._model.bmailFolderStructure); h = new TreeView(h); f = a.argsTree; f.treeId = c.id; h.createTree(f); var k = a.handleLabelClick; h.tree.subscribe("labelClick", function (a) { k(a) }); a.changeRoot && this.changeExpandRootNode(h); d && h.addActiveStyle(h.getNodeByProperty("labelElId", d));
    g.appendChild(document.createElement("div")).className = "ft"; var m = new YAHOO.widget.SimpleDialog(g, { fixedcenter: !0, modal: !0, width: "480px", constraintoviewport: !0, close: !1, visible: !1 }), d = function () { console.log(a); console.log(a.handleOk); a.handleOk(); m.cancel() }, c = [{ text: chooseButtonMsg, handler: d }, { text: closeButtonMsg, handler: function () { m.cancel() } }], h = a.addButtons || []; if (0 < h.length) for (f = 0; f < h.length; f++) c.unshift(h[f]); m.cfg.queueProperty("buttons", c); m.setHeader("<div class = 'rhd'></div><div class = 'rft'></div>" +
    e); m.render(document.body); m.bringToTop(); m.show(); document.getElementById("foldertoMove") && document.getElementById("foldertoMove").focus(); b && (document.getElementById("idSubFolder").value = b.labelElId); $("#divFolder").css("margin-left", "3px"); $(g).find("#bodyMoveDialog").css("margin-left", "10px"); $(g).find("span.button-group").css("padding-right", "19px"); m.focusEnter(d); mailbox.getInstance().disableOutline()
};
TreeController.prototype.moveFolder = function (a) {
    function b() {
        var a = document.getElementById("idSubFolder").value, b = Session.getInstance().getUser(), e = new XMLElement("FolderActionRequest"), f = e.addElement("action"); f.addAttribute("op", "move"); f.addAttribute("id", d); f.addAttribute("l", a); b = new Soap(b, e, "urn:zimbraMail", !0, sessionId, seqNotify); b.isNeedAbort(!1); b.sendRequestCallback(function (a, b) {
            if (a && a.Body && a.Body.FolderActionResponse) {
                var e = c.children, f = null; if ("1" == b) oChildNode = new YAHOO.widget.TextNode("<div class='htmlnodelabel'><a>" +
                getSubfolderLabel(c.label) + "</a></div>", tree.getRoot(), !1), oChildNode.labelStyle = "icon-subfolder", oChildNode.labelElId = d, h._view.removeNode(c), h.draw(), h.sendFolderStructure(tree.getRoot(), 1); else {
                    f = h._view.getNodeByProperty("labelElId", b); f = new YAHOO.widget.TextNode("<div class='htmlnodelabel'><a>" + getSubfolderLabel(c.label) + "</a></div>", f, !1); f.labelStyle = "icon-subfolder"; f.labelElId = d; if (0 < e.length) for (var g = 0; g < e.length; g++) e[g].appendTo(f); h._view.removeNode(c); h._view.draw(); h.sendFolderStructure(tree.getRoot(),
                    2)
                }
            } else new MessageBox(errorDlgMsg, errorMoveParentFolder)
        }, a); this.cancel()
    } var c = a.node, d = c.labelElId, f = document.body.appendChild(document.createElement("div")); f.setAttribute("id", "moveDialog"); f.appendChild(document.createElement("div")).className = "hd"; a = f.appendChild(document.createElement("div")); a.className = "bd"; var e = document.createElement("div"), g = document.createElement("label"); g.innerHTML = folderMsg + ": "; e.appendChild(g); g = document.createElement("input"); g.setAttribute("type", "text"); g.setAttribute("size",
    "30"); g.setAttribute("id", "foldertoMove"); g.setAttribute("readonly", "readonly"); e.appendChild(g); g = document.createElement("input"); g.setAttribute("type", "hidden"); g.setAttribute("id", "idSubFolder"); e.appendChild(g); a.appendChild(e); a = a.appendChild(document.createElement("div")); a.style.overflow = "auto"; a.style.background = "none repeat scroll 0 0 #FFFFFF"; a.style.height = "300px"; a.style.width = "424px"; a.style.border = "1px solid #c0c0c0"; a.style.marginTop = "5px"; a.style.bgcolor = "#fdfcfa"; a = a.appendChild(document.createElement("div"));
    a.id = TreeController.subTreeId + share.INDEX; share.INDEX++; e = new TreeModel(this._model.bmailFolderStructure); e = new TreeView(e); a = { treeId: a.id, isSubTree: !0, haveRootNode: !0 }; e.createTree(a); e.tree.subscribe("labelClick", function (a) { document.getElementById("idSubFolder").value = a.labelElId; document.getElementById("foldertoMove").value = getSubfolderLabel(a.label) }); this.changeExpandRootNode(e); e.addActiveStyle(e.getNodeByProperty("labelElId", d)); f.appendChild(document.createElement("div")).className = "ft";
    var f = new YAHOO.widget.SimpleDialog(f, { fixedcenter: !0, modal: !0, width: "450px", constraintoviewport: !0, close: !1, visible: !1 }), h = this; f.cfg.queueProperty("buttons", [{ text: chooseButtonMsg, handler: b }, { text: closeButtonMsg, handler: function () { YAHOO.util.Dom.removeClass(c.labelElId, "folderRightClick"); this.cancel() } }]); f.setHeader("<div class = 'rhd'></div><div class = 'rft'></div>" + moveFolderMsg); f.render(document.body); f.bringToTop(); f.show(); document.getElementById("foldertoMove").focus(); document.getElementById("idSubFolder").value =
    tmpNode.labelElId; f.focusEnter(b); mailbox.getInstance().disableOutline()
}; TreeController.prototype.getFirstParent = function (a) { return this._view.getFirstParent(a) };
TreeController.prototype.updateFolderStructure = function () { var a = this.tmpHiddenNode.length; if (0 < a) { for (var b = 0; b < a; b++) for (var c = this.tmpHiddenNode[b].node.labelElId, d = this.tmpHiddenNode[b].hidden, f = this._model.bmailFolderStructure.length, e = 0; e < f; e++) if (c == this._model.bmailFolderStructure[e].id) { this._model.bmailFolderStructure[e].visible = d; break } this.tmpHiddenNode = [] } };
TreeController.prototype.manageFolders = function () {
    function a() { e.updateFolderStructure(); e._view.redrawTreeView(!0); window.setTimeout(function () { e.folderStructureChange({}) }, 200); this.cancel() } this.tmpHiddenNode = []; var b = document.body.appendChild(document.createElement("div")); b.setAttribute("id", "moveDialog"); b.appendChild(document.createElement("div")).className = "hd"; var c = b.appendChild(document.createElement("div")); c.className = "bd"; c = c.appendChild(document.createElement("div")); c.setAttribute("id",
    "bodyMoveDialog"); c.style.overflow = "auto"; c.style.background = "none repeat scroll 0 0 #FFFFFF"; c.style.height = "300px"; c.style.width = "438px"; c.style.border = "1px solid #c0c0c0"; c.style.marginTop = "5px"; c.style.bgcolor = "#fdfcfa"; c = c.appendChild(document.createElement("div")); c.id = TreeController.subTreeId + share.INDEX; share.INDEX++; b.appendChild(document.createElement("div")).className = "ft"; var d = new TreeModel(this._model.bmailFolderStructure), f = new TreeView(d); f.createTree({
        treeId: c.id, isSubTree: !0, displayHiddenFolder: !0,
        dynamicLoad: !0
    }); var e = this, c = new YAHOO.widget.SimpleDialog(b, { fixedcenter: !0, modal: !0, constraintoviewport: !0, close: !1, width: "480px", visible: !1 }); c.cfg.queueProperty("buttons", [{ text: visibleButtonMsg, handler: function () { var a = f.getSelectedNode(); e.appearFolder(a); f.draw() } }, { text: hideButtonMsg, handler: function () { var a = f.getSelectedNode(); console.log(a); "2" != a.labelElId && e.hideFolder(a); f.draw() } }, { text: chooseButtonMsg, handler: a }, { text: cancelButtonMsg, handler: function () { this.cancel() } }]); c.setHeader("<div class = 'rhd'></div><div class = 'rft'></div>" +
    managmentFolderMsg); c.render(document.body); c.bringToTop(); c.showEvent.subscribe(function () { }); c.show(); $(b).find("#bodyMoveDialog").css("margin-left", "10px"); $(b).find("span.button-group").css("padding-right", "19px"); c.focusEnter(a); mailbox.getInstance().disableOutline()
};
TreeController.prototype.updateHiddenNode = function (a, b) { var c = this.tmpHiddenNode.length; if (0 < c) { for (var d = !1, f = 0; f < c; f++) if (this.tmpHiddenNode[f].node == a) { d = !0; this.tmpHiddenNode[f].hidden = b; break } d || this.tmpHiddenNode.push({ node: a, hidden: b }) } else this.tmpHiddenNode.push({ node: a, hidden: b }) }; TreeController.prototype.hideFolder = function (a) { if ("icon-hiddenfolder" != a.labelStyle && (a.labelStyle = "icon-hiddenfolder", this.updateHiddenNode(a, "0"), a.hasChildren(!0))) { a = a.children; for (var b = a.length, c = 0; c < b; c++) this.hideFolder(a[c]) } };
TreeController.prototype.appearFolder = function (a) { if ("icon-hiddenfolder" == a.labelStyle && (a.labelStyle = "icon-subfolder", this.updateHiddenNode(a, "1"), a.hasChildren(!0))) { a = a.children; for (var b = a.length, c = 0; c < b; c++) this.appearFolder(a[c]) } };
TreeController.prototype.syncFolder = function (a) {
    var b = a.node; a = b.labelStyle; if ("icon-gtalk" == a || "icon-yahoo" == a) { a = ""; for (var b = b.labelElId, c = 0; c < inboxAccount.length; c++) if (inboxAccount[c].rootId == b) { a = inboxAccount[c].imapId; break } b = new XMLElement("ImportDataRequest"); b.addElement("imap").addAttribute("id", a); a = new Soap(Session.getInstance().getUser(), b, "urn:zimbraMail", !0); a.setSimple(!1); a.isNeedAbort(!1); a.sendRequestCallback(function () { }) } currentUser.scrollToGetMail ? synchronizeInbox(0, !0) : (a =
    paging.getInstance().getStartIndex(), synchronizeInbox(a, !0))
};
TreeController.prototype.renameFolder = function (a) {
    var b = a.node; if (!this.isMainFolder(b.labelElId)) {
        a = getSubfolderLabel(b.label); var c = new convertSpecialEntities; c.setInput(a); a = c.convertToHTMLEntity(); var d = this; new ConfirmBox(renameFolderMsg, '<div style="margin-left: 10px;float:left;font-size:12px">' + inputFolderNameMsg + ': </div> <div style="margin-top: 25px"><input type="text" style="font-family: Tahoma;font-size: 12px;width: 200px;" name="txtFolder" id="txtFolder" value ="' + a + '"/>', function () {
            var a =
            $("#txtFolder").val(); if ("" == a) new MessageBox(errorDlgMsg, errorDlgMsg); else if (a && 0 < a.length) {
                var c = Session.getInstance().getUser(), g = Session.getInstance().getAuthen(), h = new convertSpecialEntities; h.setInput(a); a = h.convertToHTMLCode(); c = folderAction(c, g, "rename", b.labelElId, a); c.Body.FolderActionResponse ? (b.label = "<div class='htmlnodelabel'><a>" + a + "</a></div>", b.refresh()) : c.Body.Fault && c.Body.Fault.Detail && c.Body.Fault.Detail.Error && "mail.ALREADY_EXISTS" == c.Body.Fault.Detail.Error.Code ? new MessageBox(errorDlgMsg,
                errorFolderExistMsg) : c.Body.Fault && c.Body.Fault.Detail && c.Body.Fault.Detail.Error && "mail.INVALID_NAME" == c.Body.Fault.Detail.Error.Code ? new MessageBox(errorDlgMsg, errorInvalidNameMsg) : new MessageBox(errorDlgMsg, errorRenameFolderMsg); d.sendFolderStructure(d._view.getRoot(), 2)
            }
        }); $("#txtFolder").focus()
    }
};
TreeController.prototype.markAllReadFolder = function (a) {
    var b = a.node, c = this; a = YAHOO.util.Dom.getX(b.getElId()); var d = YAHOO.util.Dom.getY(b.getElId()); new ConfirmBox(markAllReadMsg, cfmMarkAllReadMsg, function () {
        var a = b.labelElId, e = c._view.getFirstParent(b), d = b.labelStyle; if (!e && ("icon-gtalk" == d || "icon-yahoo" == d) && 0 < inboxAccount.length) { e = -1; for (d = 0; d < inboxAccount.length; d++) if (inboxAccount[d].rootId == a) { e = d; break } a = inboxAccount[e].folderId } e = new XMLElement("FolderActionRequest"); d = e.addElement("action");
        d.addAttribute("op", "read"); d.addAttribute("id", a); e = new Soap(Session.getInstance().getUser(), e, "urn:zimbraMail", !0); e.isNeedAbort(!1); e.sendRequestCallback(function (b) {
            if (b.Body && b.Body.FolderActionResponse && b.Body.FolderActionResponse.action && b.Body.FolderActionResponse.action.id == a) {
                b = dataTable.getRecordSet(); var c = b.getLength(); if (0 < c) for (var e = 0; e < c; e++) {
                    var d = b.getRecord(e); if (1 == d.getData().unread) {
                        var g = d.getData().from.substring(3, d.getData().from.length - 4), p = d.getData().subject.substring(3,
                        d.getData().subject.length - 4), q = d.getData().date.substring(3, d.getData().date.length - 4); dataTable.updateCell(d, "mail", '<img src="giaodien/mail.png" style="margin-top:3px">', !0); dataTable.updateCell(d, "from", g, !0); dataTable.updateCell(d, "date", q, !0); dataTable.updateCell(d, "subject", p, !0); updateFlagMailForCacheFolder(a, "unread", 0, d.getData().id)
                    }
                }
            } else new MessageBox(errorDlgMsg, errorMarkAllReadMsg)
        })
    }, function () { }, chooseButtonMsg, cancelButtonMsg, "300px", a, d + 20, !1, !1)
};
TreeController.prototype.deleteFolder = function (a) {
    var b = a.node, c = b.labelElId, d = this; a = YAHOO.util.Dom.getX(b.getElId()); var f = YAHOO.util.Dom.getY(b.getElId()); new ConfirmBox(deleteFolderMsg, cfmDeleteFolderMsg, function () {
        var a = Session.getInstance().getUser(), f = Session.getInstance().getAuthen(); if (!d.isMainFolder(c)) if ("3" != b.parent.labelElId) {
            var h = b.labelStyle; "icon-gtalk" == h || "icon-yahoo" == h ? (a = YAHOO.util.Dom.getX(b.getElId()), f = YAHOO.util.Dom.getY(b.getElId()), new ConfirmBox(messageBoxMsg, deleteExtFolderMsg,
            function () {
                var a = "gtalk"; "icon-yahoo" == h && (a = "yahoo"); var c = getLocation(b); delRegistration(a, c, !0); 0 == b.depth && d._model.removeFolderIndex(b); d._view.removeNode(b); d._view.draw(); var e = "", e = "gtalk" == a ? "gtalk-" + c : "yahoo-" + c; $(".divGroup[name='" + e + "']").find(".divGroup").remove(); $(".divGroup[name='" + e + "']").children().find(".txtGroupNameLT").attr("style", "background:#fff; padding-right:5px; padding-left:8px; cursor:default; "); $(".divGroup[name='" + e + "']").children().find(".txtGroupNameLT").attr("type",
                a); "gtalk" == a ? $(".divGroup[name='" + e + "']").children().find(".txtGroupNameLT").html(googleAccountMsg) : $(".divGroup[name='" + e + "']").children().find(".txtGroupNameLT").html(yahooAccountMsg); $(".divGroup[name='" + e + "']").children().find(".txtGroupNameLT").attr("class", "txtGroupNameLTLogin"); $(".txtGroupNameLTLogin").bind("click", function (a) { showBoxLogin($(this).attr("type"), a) })
            }, function () { }, chooseButtonMsg, cancelButtonMsg, "300px", a, f + 20, !1, !1)) : (a = folderAction(a, f, "trash", c), a.Body.FolderActionResponse ?
            (d._model.removeFolderIndex(b), d._view.removeNode(b), a = d._view.getNodeByProperty("labelElId", "3"), f = new YAHOO.widget.TextNode(b.label, a, !1), f.labelElId = c, f.labelStyle = "icon-subfolder", a.refresh(), a.expand(), d._view.draw(), d._model.folderIndex.push({ path: "trash/" + b.label, index: f.index, node: f }), d.sendFolderStructure(d._view.getRoot())) : a && a.Body && a.Body.Fault && a.Body.Fault.Reason && a.Body.Fault.Reason.Text ? -1 != a.Body.Fault.Reason.Text.indexOf("no such folder id") && (d._view.removeNode(b), d._view.draw(),
            d.sendFolderStructure(d._view.getRoot(), 1)) : new MessageBox(errorDlgMsg, errorCannotDeleteMsg))
        } else "3" == b.parent.labelElId && (a = folderAction(a, f, "delete", c), a.Body.FolderActionResponse ? (d._view.removeNode(b), d._model.removeFolderIndex(b), d._view.draw(), d.sendFolderStructure(d._view.getRoot())) : a && a.Body && a.Body.Fault && a.Body.Fault.Reason && a.Body.Fault.Reason.Text ? -1 != a.Body.Fault.Reason.Text.indexOf("no such folder id") && (d._view.removeNode(b), d._view.draw(), d.sendFolderStructure(d._view.getRoot(),
        1)) : new MessageBox(errorDlgMsg, errorCannotDeleteMsg))
    }, function () { }, chooseButtonMsg, cancelButtonMsg, "300px", a, f + 20, !1, !1)
};
TreeController.prototype.createNewFolder = function (a) {
    function b() {
        var a = document.getElementById("foldertoMove").value; if ("" != a) {
            var b = document.getElementById("idSubFolder").value; "" == b && (b = "1"); if (b) {
                var c = g._view.getNodeByProperty("labelElId", b), e = Session.getInstance().getUser(), d = Session.getInstance().getAuthen(), f = new convertSpecialEntities; f.setInput(a); a = f.convertToHTMLCode(); e = createFolder(e, d, b, a); e.Body.CreateFolderResponse ? (this.cancel(), "1" == b ? (oChildNode = new YAHOO.widget.TextNode("<div class='htmlnodelabel'><a>" +
                a + "</a></div>", g._view.getRoot(), !1), oChildNode.labelStyle = "icon-subfolder", oChildNode.labelElId = e.Body.CreateFolderResponse.folder[0].id, g._view.draw(), g._model.folderIndex.push({ path: a, index: oChildNode.index, node: oChildNode })) : (oChildNode = new YAHOO.widget.TextNode("<div class='htmlnodelabel'><a>" + a + "</a></div>", c, !1), oChildNode.labelStyle = "icon-subfolder", oChildNode.labelElId = e.Body.CreateFolderResponse.folder[0].id, c.expand(), c.refresh(), g._view.draw(), g._model.folderIndex.push({
                    path: getLocation(c) +
                    "/" + a, index: oChildNode.index, node: oChildNode
                })), g.sendFolderStructure(g._view.getRoot())) : (e.Body.Fault && e.Body.Fault.Detail && e.Body.Fault.Detail.Error && "mail.ALREADY_EXISTS" == e.Body.Fault.Detail.Error.Code ? new MessageBox(errorDlgMsg, errorFolderExistMsg) : e.Body.Fault && e.Body.Fault.Detail && e.Body.Fault.Detail.Error && "service.PERM_DENIED" == e.Body.Fault.Detail.Error.Code ? new MessageBox(errorDlgMsg, errorNoPermissionMsg) : new MessageBox(errorDlgMsg, errorCreateNewFolder), this.cancel())
            }
        } else new MessageBox(errorDlgMsg,
        errorNoInputFolderName); this.cancel()
    } var c = a.node.labelElId; YAHOO.util.Dom.removeClass(c, "folderRightClick"); var d = document.body.appendChild(document.createElement("div")); d.setAttribute("id", "moveDialog"); d.appendChild(document.createElement("div")).className = "hd"; a = d.appendChild(document.createElement("div")); a.className = "bd"; var f = document.createElement("div"), e = document.createElement("label"); e.innerHTML = folderMsg + ": "; f.appendChild(e); e = document.createElement("input"); e.setAttribute("type",
    "text"); e.setAttribute("size", "30"); e.setAttribute("id", "foldertoMove"); f.appendChild(e); e = document.createElement("input"); e.setAttribute("type", "hidden"); e.setAttribute("id", "idSubFolder"); f.appendChild(e); a.appendChild(f); a = a.appendChild(document.createElement("div")); a.style.overflow = "auto"; a.style.background = "none repeat scroll 0 0 #FFFFFF"; a.style.height = "200px"; a.style.width = "400px"; a.style.border = "1px solid #c0c0c0"; a.style.marginTop = "5px"; a.style.bgcolor = "#fdfcfa"; a = a.appendChild(document.createElement("div"));
    a.id = TreeController.subTreeId + share.INDEX; share.INDEX++; f = new TreeModel(this._model.bmailFolderStructure); f = new TreeView(f); a = { treeId: a.id, isSubTree: !0, haveRootNode: !0 }; f.createTree(a); f.addActiveStyle(f.getNodeByProperty("labelElId", c)); this.changeExpandRootNode(f); f.tree.subscribe("labelClick", function (a) { document.getElementById("idSubFolder").value = a.labelElId }); d.appendChild(document.createElement("div")).className = "ft"; var g = this, d = new YAHOO.widget.SimpleDialog(d, {
        fixedcenter: !0, modal: !0, width: "428px",
        constraintoviewport: !0, close: !1, visible: !1
    }); d.cfg.queueProperty("buttons", [{ text: chooseButtonMsg, handler: b }, { text: closeButtonMsg, handler: function () { this.cancel() } }]); d.setHeader("<div class = 'rhd'></div><div class = 'rft'></div>" + createNewFolderMsg); d.render(document.body); d.bringToTop(); d.show(); document.getElementById("foldertoMove").focus(); document.getElementById("idSubFolder").value = c; d.focusEnter(b); mailbox.getInstance().disableOutline()
};
TreeController.prototype.postToThisFolder = function () {
    YAHOO.util.Get.script(["js/editor/js/editor.js.zgz", "js/Compose/compose.js.zgz"],
        {
            onFailure: function () { }, onSuccess: function () {
                var a = new ComposeModel({
                    composeType: "postfolder"
                }),
                    b = new ComposeView(a);
                (new ComposeController(b, a)).init()
            }
        })
};
TreeController.prototype.emptyFolder = function (a) {
    var b = a.node, c = this; a = YAHOO.util.Dom.getX(b.getElId()); var d = YAHOO.util.Dom.getY(b.getElId()); new ConfirmBox(emptyFolderMsg, emptyContentFolderMsg, function () {
        var a = b.labelElId, e = c._view.getFirstParent(b), d = b.labelStyle; if (!e && ("icon-gtalk" == d || "icon-yahoo" == d) && 0 < inboxAccount.length) { e = -1; for (d = 0; d < inboxAccount.length; d++) if (inboxAccount[d].rootId == a) { e = d; break } a = inboxAccount[e].folderId } e = new XMLElement("FolderActionRequest"); d = e.addElement("action");
        d.addAttribute("op", "empty"); d.addAttribute("id", a); e = new Soap(Session.getInstance().getUser(), e, "urn:zimbraMail", !0); e.isNeedAbort(!1); e.sendRequestCallback(function (b) {
            "3" == a ? b.Body && b.Body.FolderActionResponse && b.Body.FolderActionResponse.action && b.Body.FolderActionResponse.action.id == a ? (paging.getInstance().setTotalRecords(0, !0), dataTable.deleteRows(0, dataTable.getRecordSet().getLength()), b = YAHOO.widget.LayoutUnit.getLayoutUnitById("bottom5"), b.set("body", ""), b = c._view.getNodeByProperty("labelElId",
            "3"), b.refresh(), b.expand(), c._view.removeChildren(b), c._view.draw(), c.sendFolderStructure(c._view.getRoot(), 2)) : new MessageBox(errorDlgMsg, errorCannotDeleteMsg) : b.Body && b.Body.FolderActionResponse && b.Body.FolderActionResponse.action && b.Body.FolderActionResponse.action.id == a ? (paging.getInstance().setTotalRecords(0, !0), dataTable.deleteRows(0, dataTable.getRecordSet().getLength()), b = YAHOO.widget.LayoutUnit.getLayoutUnitById("bottom5"), b.set("body", "")) : new MessageBox(errorDlgMsg, errorCannotDeleteMsg)
        })
    },
    function () { }, chooseButtonMsg, cancelButtonMsg, "300px", a, d + 20, !1, !1)
}; TreeController.prototype.draw = function () { this._view.draw() };
TreeController.prototype.sendFolderStructure = function (a, b) {
    for (var c = [], d = new convertSpecialEntities, f = this._model.folderIndex.length, e = 0; e < f; e++) {
        var g = this._model.folderIndex[e].node, h = g.labelElId, l = getNodeLocation(g); d.setInput(l); var l = d.convertToHTMLCode(), k = g.labelStyle, m = 1, n = "", n = g.parent && g.parent.labelElId ? g.parent.labelElId : "1", p = 0; g.expanded && (p = 1); if ("icon-gtalk" == k || "icon-yahoo" == k) l = "icon-gtalk" == k ? l + "@gmail.com" : l + "@yahoo.com"; c.push({
            folderId: h, folderPath: l, parentId: n, hidden: m, link: 0,
            expanded: p
        })
    } Session.getInstance().getUser(); Session.getInstance().getAuthen(); f = this._model.bmailFolderStructure.length; for (e = 0; e < f; e++) "0" == this._model.bmailFolderStructure[e].visible && (h = this._model.bmailFolderStructure[e].id, l = this._model.bmailFolderStructure[e].path, d.setInput(l), l = d.convertToHTMLCode(), m = 0, n = this._model.bmailFolderStructure[e].parentId, p = 0, c.push({ folderId: h, folderPath: l, parentId: n, hidden: m, link: 0, expanded: p })); b ? this._model.sendChangeFolderStructureRequest(c, b) : this._model.sendChangeFolderStructureRequest(c)
};
TreeController.prototype.changeExpandRootNode = function (a) {
    var b = a.tree; a = b.getRoot().children[0]; var c = a.getElId().substring(4), c = parseInt(c), d = "ygtvt" + c, f = location.protocol + "//" + location.hostname; "1" == a.labelElId ? (YAHOO.util.Dom.setStyle(d, "background-image", 'url("' + f + '/mail/giaodien/collapse.png")'), YAHOO.util.Dom.setStyle(d, "background-position", "23px 5px"), b.subscribe("expand", function (a) {
        a == b.getNodeByProperty("labelElId", "1") && (YAHOO.util.Dom.setStyle(d, "background-image", 'url("' + f + '/mail/giaodien/collapse.png")'),
        YAHOO.util.Dom.setStyle(d, "background-position", "23px 5px"))
    }), b.subscribe("collapse", function (a) { a == b.getNodeByProperty("labelElId", "1") && (YAHOO.util.Dom.setStyle(d, "background-image", 'url("' + f + '/mail/giaodien/expand.png")'), YAHOO.util.Dom.setStyle(d, "background-position", "23px 5px")) })) : (-1 != YAHOO.util.Dom.getStyle(d, "background-image").indexOf("tp.gif") ? YAHOO.util.Dom.setStyle(d, "background-image", 'url("' + f + '/mail/yui/build/treeview/assets/skins/sam/tph.gif")') : YAHOO.util.Dom.setStyle(d, "background-image",
    'url("' + f + '/mail/yui/build/treeview/assets/skins/sam/tmh.gif")'), b.subscribe("collapse", function (a) { YAHOO.util.Dom.setStyle(d, "background-image", 'url("' + f + '/mail/yui/build/treeview/assets/skins/sam/tph.gif")') }), b.subscribe("expand", function (a) { YAHOO.util.Dom.setStyle(d, "background-image", 'url("' + f + '/mail/yui/build/treeview/assets/skins/sam/tmh.gif")') }))
}; function TreeDragDrop() { } TreeDragDrop.PREFIX_NODE_DRAG = "ygtvcontentel"; TreeDragDrop.prototype.init = function () { new YAHOO.util.DDTarget(TreeView.treeId) }; function DDList(a, b, c) { DDList.superclass.constructor.call(this, a, b, c); a = this.getDragEl(); YAHOO.util.Dom.setStyle(a, "opacity", .67); this.goingUp = !1; this.lastY = 0 }
YAHOO.extend(DDList, YAHOO.util.DDProxy, {
    destNode: null, sourceNode: null, checkGoingUp: !1, sourceNodeSibling: [], destNodeSibling: [], startDrag: function (a, b) { TreeController.getInstance(); var c = this.getDragEl(), d = this.getEl(); c.innerHTML = d.innerHTML; YAHOO.util.Dom.setStyle(c, "width", "auto"); YAHOO.util.Dom.setStyle(c, "background-color", "#fff"); YAHOO.util.Dom.setStyle(c, "border", "0px") }, endDrag: function (a) {
        var b = TreeController.getInstance()._model.folderIndex; TreeController.getInstance(); var c = TreeController.getInstance(),
        d = this.getEl(); a = this.getDragEl(); YAHOO.util.Dom.setStyle(a, "visibility", ""); var d = new YAHOO.util.Motion(a, { points: { to: YAHOO.util.Dom.getXY(d) } }, .2, YAHOO.util.Easing.easeOut), f = a.id, e = this.id; d.onComplete.subscribe(function () {
            $(".rightFolder").removeClass("rightFolder"); YAHOO.util.Dom.setStyle(f, "visibility", "hidden"); YAHOO.util.Dom.setStyle(e, "visibility", ""); for (var a = 0, d = 0, l = b.length, k = 0; k < l; k++) b[k].node == destNode && (a = k), b[k].node == sourceNode && (d = k); var m = [], k = !1; a < d && (k = !0); if (k) {
                if (0 < a) for (k =
                0; k < a; k++) m.push(b[k]); m.push(b[a]); m.push(b[d]); for (k = a + 1; k < d; k++) m.push(b[k]); if (d < l - 1) for (k = d + 1; k < l; k++) m.push(b[k])
            } else { if (0 < d) for (k = 0; k < d; k++) m.push(b[k]); for (k = d + 1; k < a; k++) m.push(b[k]); m.push(b[a]); m.push(b[d]); if (a < l - 1) for (k = a + 1; k < l; k++) m.push(b[k]) } console.log("tempFolderIndex:" + m); TreeController.getInstance()._model.folderIndex = m; c.sendFolderStructure(c._view.getRoot(), 2); $("#" + labelId).children(".htmlnodelabel").css("color", "#000")
        }); d.animate()
    }, onDragDrop: function (a, b) {
        if (1 === YAHOO.util.DragDropMgr.interactionInfo.drop.length &&
        !YAHOO.util.DragDropMgr.interactionInfo.sourceRegion.intersect(YAHOO.util.DragDropMgr.interactionInfo.point)) { var c = YAHOO.util.DragDropMgr.getDDById(b), d = b.replace(TreeDragDrop.PREFIX_NODE_DRAG, ""), f = YAHOO.util.Dom.get("ygtv" + d), d = this.getEl().id.replace(TreeDragDrop.PREFIX_NODE_DRAG, ""), d = YAHOO.util.Dom.get("ygtv" + d); this.getSibling(); YAHOO.util.Dom.insertAfter(d, f); this.insertAfter(sourceNode, destNode); this.getSibling(); c.isEmpty = !1; this.refreshTree(); YAHOO.util.DragDropMgr.refreshCache() }
    }, insertBefore: function (a,
    b) { var c = b.parent; if (c) { a.tree && a.tree.popNode(a); var d = b.isChildOf(c); c.children.splice(d, 0, a); b.previousSibling && (b.previousSibling.nextSibling = a); a.previousSibling = b.previousSibling; a.nextSibling = b; b.previousSibling = a; a.applyParent(c) } }, insertAfter: function (a, b) {
        var c = b.parent; if (c) {
            a.tree && a.tree.popNode(a); var d = b.isChildOf(c); if (!b.nextSibling) return a.nextSibling = null, a.appendTo(c); c.children.splice(d + 1, 0, a); b.nextSibling.previousSibling = a; a.previousSibling = b; a.nextSibling = b.nextSibling; b.nextSibling =
            a; a.applyParent(c)
        }
    }, getSibling: function () { this.sourceNodeSibling.push(sourceNode.nextSibling); this.sourceNodeSibling.push(sourceNode.previousSibling); this.sourceNodeSibling.push(sourceNode); this.destNodeSibling.push(destNode.nextSibling); this.destNodeSibling.push(destNode.previousSibling); this.destNodeSibling.push(destNode) }, refreshTree: function () {
        for (var a = 0; a < this.sourceNodeSibling.length; a++) { var b = this.sourceNodeSibling[a]; b && this.refreshNode(b) } for (a = 0; a < this.destNodeSibling.length; a++) (b = this.destNodeSibling[a]) &&
        this.refreshNode(b); this.sourceNodeSibling = []; this.destNodeSibling = []
    }, refreshNode: function (a) {
        var b = a.getElId(), b = b.replace("ygtv", ""), b = "ygtvt" + b; a.hasChildren() && !a.nextSibling && a.previousSibling ? (this.refreshClass(b), $("#" + b).addClass("ygtvlp")) : 0 == a.depth && a.hasChildren() && a.nextSibling && !a.previousSibling ? (this.refreshClass(b), $("#" + b).addClass("ygtvtph")) : 0 != a.depth && a.hasChildren() && a.nextSibling && !a.previousSibling ? (this.refreshClass(b), $("#" + b).addClass("ygtvtp")) : a.hasChildren() && a.previousSibling &&
        a.nextSibling ? (this.refreshClass(b), $("#" + b).addClass("ygtvtp")) : !a.hasChildren() && a.nextSibling && a.previousSibling ? (this.refreshClass(b), $("#" + b).addClass("ygtvtn")) : a.hasChildren() || a.nextSibling || !a.previousSibling ? 0 != a.depth || a.hasChildren() || !a.nextSibling || a.previousSibling ? 0 == a.depth || a.hasChildren() || !a.nextSibling || a.previousSibling || (this.refreshClass(b), $("#" + b).addClass("ygtvtn")) : (this.refreshClass(b), $("#" + b).addClass("ygtvtnh")) : (this.refreshClass(b), $("#" + b).addClass("ygtvln"))
    }, refreshClass: function (a) {
        $("#" +
        a).removeClass(); $("#" + a).addClass("ygtvcell")
    }, onDrag: function (a) { a = YAHOO.util.Event.getPageY(a); a < this.lastY ? this.goingUp = !0 : a > this.lastY && (this.goingUp = !1); this.lastY = a }, onDragOver: function (a, b) {
        TreeController.getInstance(); var c = TreeController.getInstance()._model.oTextNodeMap; TreeController.getInstance(); var d = this.getEl(), f = YAHOO.util.Dom.get(b); f && f.id.match(/^ygtvcontentel[0-9]+$/) && (d = d.id.match(/[0-9]+$/), sourceNode = c["ygtvcontentel" + d[0]], d = f.id.match(/[0-9]+$/), destNode = c["ygtvcontentel" +
        d[0]], $(".rightFolder").removeClass("rightFolder"), $("#" + f.id).addClass("rightFolder"), YAHOO.util.DragDropMgr.refreshCache())
    }
}); function TreeModel(a, b, c) { this.bmailFolderStructure = a || []; this.bmailFolderStructureChanged = c || "FALSE"; this.accountArray = []; this.inboxAccount = []; this.linkExpires = []; this.linkShare = []; this.folderInfo = []; this.folderIndex = []; this.oTextNodeMap = {}; this.initObServerEvent(); this.sync = !1; this.versionStructure = b || 0; this.fistVersionStructure = b || 0; this.structureString = this.eOVersion = ""; this.needSyncFolder = !1; this.getVersionTreeStructure(); this.changeFolderStructureXhr = null } TreeModel.SYNC_URL = "https://webeoffice.bkav.com/SyncBmail.asmx/SyncFolder";
TreeModel.CHECKVERSION_URL = "https://webeoffice.bkav.com/SyncBmail.asmx/CheckUpdateFromBmail"; TreeModel.prototype.initObServerEvent = function () { this.initCalendarEvent = new Event(this); this.initContactEvent = new Event(this); this.initFacebookEvent = new Event(this); this.redrawTreeViewEvent = new Event(this) }; TreeModel.prototype.getBmailFolderStructure = function () { return this.bmailFolderStructure }; TreeModel.prototype.set = function (a, b) { this[a] = b }; TreeModel.prototype.getAccountArray = function () { return this.accountArray };
TreeModel.prototype.setBmailFolderStructure = function (a) { this.bmailFolderStructure = a }; TreeModel.getlastUpdate = function () { var a = new Date, b = a.getMonth() + 1, c = a.getFullYear(), d = a.getDate(), f = "AM", e = a.getHours(); 12 < e && (f = "PM", e -= 12); var g = a.getMinutes(), a = a.getSeconds(); return b + "/" + d + "/" + c + ", " + e + ":" + g + ":" + a + " " + f };
TreeModel.prototype.getVersionTreeStructure = function () {
    if ("tool" == mailbox.getInstance().client) {
        var a = this.versionStructure || "12/26/2014, 4:58:33 PM", b = this; "dungha" != readCookie("bkavUsername") && "dunghv" != readCookie("bkavUsername") && "cuongnt" != readCookie("bkavUsername") && "congntb" != readCookie("bkavUsername") && "hungpv" != readCookie("bkavUsername") || $.ajax({
            url: TreeModel.CHECKVERSION_URL, data: { email: readCookie("bkavUsername"), lastBmailUpdate: a }, type: "POST", success: function (a) {
                var d = $("UpdateFromBmail",
                a), d = d[0], d = d.textContent; "false" == d && (b.needSyncFolder = !0); d = $("Version", a); d = d[0]; d = d.textContent; b.versionStructure = d; console.log("_this.versionStructure: " + b.versionStructure); b.updateVersionStructure(); window.setTimeout(function () { console.log("this.needSyncFolder: " + b.needSyncFolder); b.sync || "tool" != mailbox.getInstance().client || b.syncFolderStructureRequest(b.structureString); b.structureString = null }, 3E4)
            }, error: function () { }
        })
    }
};
TreeModel.prototype.updateVersionStructure = function () { var a = new XMLElement("ModifyVersionInfoRequest"); a.addAttribute("bmailFolderStructureVersion", this.versionStructure); a = new Soap(Session.getInstance().getUser(), a, "urn:zimbraAccount", !0); a.setSimple(!0); a.isNeedAbort(!1); a.sendRequestCallback(function (a) { }) };
TreeModel.prototype.syncFolderStructureRequest = function (a) {
    var b = this; "dungha" != readCookie("bkavUsername") && "dunghv" != readCookie("bkavUsername") && "cuongnt" != readCookie("bkavUsername") && "tienbv" != readCookie("bkavUsername") && "hopcv" != readCookie("bkavUsername") && "tamdn" != readCookie("bkavUsername") && "trinhnvd" != readCookie("bkavUsername") && "quangp" != readCookie("bkavUsername") && "dambv" != readCookie("bkavUsername") && "dungnvl" != readCookie("bkavUsername") || $.ajax({
        url: TreeModel.SYNC_URL, data: {
            email: readCookie("bkavUsername"),
            lastUpdate: b.fistVersionStructure, contentFolderBmail: "<folders>" + a + "</folders>"
        }, type: "POST", success: function (a) { console.log(a); a = $("string", a); a.text() ? (a = Util.toJson(a.text()), b.handleSyncFolderStructureData(a)) : b.sync = !0 }, error: function () { b.sync = !0 }
    })
}; TreeModel.prototype.getNewFolderSync = function (a) { };
TreeModel.prototype.handleSyncFolderStructureData = function (a) {
    for (var b = a.length, c = [], d = 0; d < b; d++) {
        var f = a[d], e = f.BmailFolderId, g = f.BmailFolderPath, h = g.split("/"), l = f.BmailIsLink, k = f.BmailIsClientNotification, m = f.BmailIsServerNotification, n = f.BmailIsVisible, f = f.BmailParentId; e && "null" != e || this.getNewFolderSync(g); g = { path: g, id: e, link: l, folderName: h[h.length - 1], visible: n, parentId: f, isServerConfigNotify: m, isClientConfigNotify: k, isOpen: 0 }; "1" == k ? Tool.getInstance().setFolderNotify(e, "0") : Tool.getInstance().setFolderNotify(e,
        "1"); c.push(g); f = new Folder(g); FolderCache.getInstance().push(f)
    } this.bmailFolderStructure = c; this.redrawTreeViewEvent.notify(); "tool" == mailbox.getInstance().client && Tool.getInstance().getConfigFolder()
}; TreeModel.prototype.handleLastCacheData = function (a) { if (a = this.getLastOfCacheData(a)) { var b = a.folderId; checkFolderCache(b) && (FolderCache.getInstance().set(b, "lastFocus", a.lastFocus), FolderCache.getInstance().set(b, "lastMessage", a.message)) } };
TreeModel.prototype.handleSelectFolder = function (a) {
    var b = this, c = a.node; a = a.mailId || ""; var d = c.labelElId; if ("icon-calendar" == c.labelStyle) b.initCalendarEvent.notify(); else if ("icon-contact" == c.labelStyle) b.initContactEvent.notify(); else if ("icon-facebook" == c.labelStyle) c = getindexTabByLabel(facebookMsg), -1 != c && tabView.set("activeIndex", c), -1 == c && YAHOO.util.Get.script(["js/facebook/facebook.js.zgz"], { onFailure: function () { }, onSuccess: function () { b.initFacebookEvent.notify() } }), YAHOO.util.Dom.setStyle(document.body,
    "cursor", "default"); else if ("icon-gtalk" != c.labelStyle && "icon-yahoo" != c.labelStyle) {
        var f = !1; if ("icon-subfolder" == c.labelStyle || "icon-publicfolder" == c.labelStyle || "icon-sharefolder" == c.labelStyle) { for (var e = c.depth, g = c, h = 0; h < e; h++) g = g.parent; if ("icon-gtalk" == g.labelStyle || "icon-yahoo" == g.labelStyle) f = !0 } f || (currentFolder = this.getCurrentFolder(c), dataTable.deleteAllRows(), f = !1, mailbox.getInstance().listMailStorage && mailbox.getInstance().listMailStorage.get(d) && (f = !0), checkFolderCache(d) || f ? this.handleFolderExistCache(c,
        a) : (currentFolderId = d, loadMail(currentFolder, a)))
    }
}; TreeModel.prototype.handleFolderExistCache = function (a, b) { var c = a.labelElId, d = this.getCurrentFolder(a), f = null; mailbox.getInstance().listMailStorage && (f = mailbox.getInstance().listMailStorage.getCurrentList(c)); var e = [], e = f ? f : FolderCache.getInstance().get(c, "Data"), f = !0; currentFolderId == c && (f = !1); currentFolder = d; currentFolderId = c; dataTable.updateData(e, f, b); updatePaginator(currentFolderId); YAHOO.util.Dom.setStyle(document.body, "cursor", "default") };
TreeModel.prototype.removeFolderIndex = function (a) { var b = [], c = this.folderIndex.length; if (0 < c) { for (var d = 0; d < c; d++) this.folderIndex[d].node != a && b.push(this.folderIndex[d]); this.folderIndex = b } }; TreeModel.prototype.getCurrentFolder = function (a) { var b = "inbox"; "2" == a.labelElId ? b = "inbox" : "5" == a.labelElId ? b = "sent" : "4" == a.labelElId ? b = "junk" : "6" == a.labelElId ? b = "drafts" : "3" == a.labelElId ? b = "trash" : (b = getLocation(a), a = new convertSpecialEntities, a.setInput(b), b = a.convertToHTMLEntity()); return b };
TreeModel.prototype.sendChangeFolderStructureRequest = function (a, b) {
    0 != currentUser.timeoutSession && window.clearTimeout(mailbox.getInstance().timeout); var c = Session.getInstance().getIpServer(), d = Session.getInstance().getAuthen(), f = Session.getInstance().getUser(), c = location.protocol + "//" + c + "/service/soap", e; e = "<soap:Envelope xmlns:soap='http://www.w3.org/2003/05/soap-envelope'><soap:Header><context xmlns='urn:zimbra'>"; e += "<format type='js'/>"; e += "<account by='name'>" + f + "</account>"; e += "<authToken >" +
    d + "</authToken>"; e += "</context>"; e += "</soap:Header>"; e += "<soap:Body>"; e += "<FolderStructureChangeRequest xmlns='urn:zimbraMail'>"; e += "<account by='name'>" + f + "</account>"; e += "<authToken >" + d + "</authToken>"; for (var d = a.length, f = "", g = 0; g < d; g++) {
        var h = -1, l = a[g].folderPath; a[g].expanded && (h = a[g].expanded); var k = Util.getNotify(a[g].folderId); -1 == h ? (e += "<folder path='" + l + "'  id='" + a[g].folderId + "' visible='" + a[g].hidden + "' link='" + a[g].link + "' parentid ='" + a[g].parentId + "'></folder>", f += "<folder path='" +
        l + "'  id='" + a[g].folderId + "' visible='" + a[g].hidden + "' link='" + a[g].link + "' parentid ='" + a[g].parentId + "' notify ='" + k + "'></folder>") : (e += "<folder path='" + l + "'  id='" + a[g].folderId + "' visible='" + a[g].hidden + "' isOpen='" + h + "' link='" + a[g].link + "' parentid ='" + a[g].parentId + "'></folder>", f += "<folder path='" + l + "'  id='" + a[g].folderId + "' visible='" + a[g].hidden + "' isOpen='" + h + "' link='" + a[g].link + "' parentid ='" + a[g].parentId + "' notify ='" + k + "'></folder>")
    } this.structureString = f; this.sync && (this.versionStructure =
    (new Date).getTime(), this.updateVersionStructure()); e += "</FolderStructureChangeRequest>"; e += "</soap:Body>"; e += "</soap:Envelope>"; d = getRequest(); d.onreadystatechange = this.handleChangeFolderStructureCallback(d); d.open("POST", c, !0, "FolderStructureChangeRequest"); d.send(e)
};
TreeModel.prototype.handleChangeFolderStructureCallback = function (a) {
    if (4 == a.readyState && 200 == a.status) {
        a = Util.toJson(a.responseText); for (var b = [], c = a.Body.FolderStructureChangeResponse.l.length, d = 0; d < c; d++) {
            var f = a.Body.FolderStructureChangeResponse.l[d].path, e = a.Body.FolderStructureChangeResponse.l[d].id, g = a.Body.FolderStructureChangeResponse.l[d].visible, h = a.Body.FolderStructureChangeResponse.l[d].link, l = a.Body.FolderStructureChangeResponse.l[d].parentid, k = "0"; a.Body.FolderStructureChangeResponse.l[d].isOpen &&
            (k = a.Body.FolderStructureChangeResponse.l[d].isOpen); b.push({ path: f, id: e, visible: g, link: h, parentId: l, isOpen: k }); this.bmailFolderStructure = b
        }
    }
}; TreeModel.prototype.abortChangeFolderStructureXhr = function () { this.changeFolderStructureXhr && this.changeFolderStructureXhr.abort(); this.changeFolderStructureXhr = null };
TreeModel.prototype.getLastOfCacheData = function (a) {
    var b = null, c = 0; currentUser.scrollToGetMail || (c = dataTable.getStartIndex()); if (0 == c) {
        c = a.labelElId; if (("icon-yahoo" == a.labelStyle || "icon-gtalk" == a.labelStyle) && 0 < inboxAccount.length) { for (var d = -1, f = 0; f < inboxAccount.length; f++) if (inboxAccount[f].rootId == a.labelElId) { d = f; break } -1 != d && (c = inboxAccount[d].folderId) } a = MailCache.getInstance().getLastFocus(); d = MailCache.getInstance().getLastRowId(); (d = MailCache.getInstance().getMessageByRowId(d)) && (b = {
            folderId: c,
            lastFocus: a, message: d
        })
    } return b
}; TreeModel.prototype.updateFolderStructureChanged = function () { var a = Session.getInstance().getUser(), b = new XMLElement("UpdateFolderStructureChangedRequest"); b.addElement("account").setText(a); a = new Soap(a, b, "urn:zimbraMail", !0); a.setSimple(!1); a.isNeedAbort(!1); a.sendRequestCallback(function () { }) }; function TreeView(a) { this._model = a; this.selectedNode = this.serviceNode = this.contactNode = this.calendarNode = this.hiddenServiceNode = null; this.displayHiddenFolder = this.haveRootNode = this.isSubTree = !1; this.expandClick = !0; this.arrInspectNode = []; this.initObserverEvent(); this.existNode = [] } TreeView.TREE_ID = "treeDiv1"; TreeView.CONTEXT_MENU_ID = "treecontextmenu";
TreeView.prototype.initObserverEvent = function () {
    this.selectFolderEvent = new Event(this);
    this.postToThisFolderEvent = new Event(this);
    this.syncFolderEvent = new Event(this);
    this.createNewFolderEvent = new Event(this);
    this.deleteFolderEvent = new Event(this); this.moveFolderEvent = new Event(this); this.renameFolderEvent = new Event(this); this.createPublicFolderEvent = new Event(this); this.createShareFolderEvent = new Event(this); this.editFolderEvent = new Event(this); this.markAllReadFolderEvent = new Event(this); this.manageFoldersEvent =
    new Event(this); this.emptyFolderEvent = new Event(this); this.folderStructureChangeEvent = new Event(this)
};
TreeView.prototype.createTree = function (a) {
    var b = this; YAHOO.util.Event.onDOMReady(function () {
        var c = a.treeId || TreeView.TREE_ID; b.isSubTree = a.isSubTree || !1; b.haveRootNode = a.haveRootNode || !1; b.displayHiddenFolder = a.displayHiddenFolder || !1; b.dynamicLoad = a.dynamicLoad || !1; b.tree = new YAHOO.widget.TreeView(c); b.dynamicLoad && b.tree.setDynamicLoad(b.dynamicLoadChildNode, 1); var d = b.getRoot(); b.haveRootNode && (d = new YAHOO.widget.TextNode("<div class='htmlnodelabel'><a>Root</a></div>", b.tree.getRoot(), !0), d.labelElId =
        "1", d.labelStyle = "icon-subfolder"); b.tree.singleNodeHighlight = !0; b.tree.subscribe("clickEvent", function (a) { b.handleSelectFolder(a.node); return !1 }); b.isSubTree || (b.tree.subscribe("expand", function (a) { a == b.serviceNode && (b.expandServiceNode(a), YAHOO.util.Dom.setStyle(b.hiddenServiceNode.getElId(), "display", "none")) }), b.tree.subscribe("collapse", function (a) { a == b.serviceNode && (b.collapseServiceNode(a), YAHOO.util.Dom.setStyle(b.hiddenServiceNode.getElId(), "display", "none")) }), b.tree.subscribe("collapseComplete",
        function (a) { b.expandClick && a != b.serviceNode && (a = { rootNode: b.getRoot() }, b.folderStructureChangeEvent.notify(a)) }), b.tree.subscribe("expandComplete", function (a) { b.expandClick && a != b.serviceNode && (a = { rootNode: b.getRoot() }, b.folderStructureChangeEvent.notify(a)); dataTable.initDragDrop(); b.setIEStyle() })); mailbox.getInstance().loadFromBpr || b.buildChildNode(d); b.isSubTree && $("#" + c).css("width", "150%")
    })
}; TreeView.prototype.getTreeDefinition = function () { return this.tree.getTreeDefinition() };
TreeView.prototype.createOriginalTree = function () { var a = this.tree.getRoot(); this.insertNode("inbox", "H\u1ed9p th\u01b0 \u0111\u1ebfn", "2", "icon-inbox", a); this.insertNode("sent", "H\u1ed9p th\u01b0 \u0111i", "5", "icon-send", a); this.insertNode("drafts", "Th\u01b0 \u0111ang so\u1ea1n", "6", "icon-draft", a); this.insertNode("junk", "Th\u01b0 r\u00e1c", "4", "icon-junk", a); this.insertNode("trash", "Th\u01b0 \u0111\u00e3 x\u00f3a", "3", "icon-trash", a); this.tree.draw() };
TreeView.prototype.expandServiceNode = function (a) { mailbox.getInstance().isEgov || (a = a.getElId().substring(4), a = parseInt(a), YAHOO.util.Dom.setStyle("ygtvt" + a, "background-image", 'url("' + (location.protocol + "//" + location.hostname) + '/mail/yui/build/treeview/assets/skins/sam/tm.gif")'), $("#serviceNode .htmlnodelabel").css("margin-top", "5px"), YAHOO.util.Dom.setStyle(this.calendarNode.getElId(), "display", "block"), YAHOO.util.Dom.setStyle(this.contactNode.getElId(), "display", "block"), this.sendRequestCollapseExpandServiceNode("TRUE")) };
TreeView.prototype.collapseServiceNode = function (a) { mailbox.getInstance().isEgov || (a = a.getElId().substring(4), a = parseInt(a), YAHOO.util.Dom.setStyle("ygtvt" + a, "background-image", 'url("' + (location.protocol + "//" + location.hostname) + '/mail/yui/build/treeview/assets/skins/sam/lp.gif")'), $("#serviceNode .htmlnodelabel").css("margin-top", "4px"), YAHOO.util.Dom.setStyle(this.calendarNode.getElId(), "display", "none"), YAHOO.util.Dom.setStyle(this.contactNode.getElId(), "display", "none"), this.sendRequestCollapseExpandServiceNode("FALSE")) };
TreeView.prototype.sendRequestCollapseExpandServiceNode = function (a) { var b = Session.getInstance().getUser(), c = new XMLElement("ModifyPrefsRequest"), d = c.addElement("pref"); d.addAttribute("name", "zimbraPrefFolderTreeOpen"); d.setText(a); (new Soap(b, c, "urn:zimbraAccount", !0, sessionId, seqNotify)).sendRequestCallback(function (a, b) { mailbox.getInstance().serviceExpandState = b }, a) };
TreeView.prototype.setIEStyle = function () {
    "Microsoft Internet Explorer" == navigator.appName && ($(".activeNode").mouseover(function () { $(this).css("color", "#fff") }), $(".activeNode").mouseout(function () { $(this).css("color", "#fff") }), $(".htmlnodelabel a").mouseover(function () { $(this).closest(".htmlnodelabel").hasClass("activeNode") || $(this).css("color", "#0A54FF") }), $(".htmlnodelabel a").mouseout(function () { $(this).closest(".htmlnodelabel").hasClass("activeNode") || $(this).css("color", "#000") }), $(".htmlnodelabel b").mouseover(function () {
        $(this).css("color",
        "#0A54FF")
    }), $(".htmlnodelabel b").mouseout(function () { $(this).css("color", "#000") }), mailbox.getInstance().disableOutline())
};
TreeView.prototype.buildChildNode = function (a, b) {
    this.existNode = []; for (var c = this._model.bmailFolderStructure.length, d = [], f = [], e = 0; e < c; e++) {
        var g = this._model.bmailFolderStructure[e].path, h = this._model.bmailFolderStructure[e].id, l = this._model.bmailFolderStructure[e].visible, k = "0"; this._model.bmailFolderStructure[e].isOpen && (k = this._model.bmailFolderStructure[e].isOpen); -1 == g.indexOf("/") ? "2" == h ? this.insertNode(inboxMsg, "2", g, "icon-inbox", a, e, k, d, !1, l) : "5" == h ? this.insertNode(sentMsg, "5", g, "icon-send",
        a, e, k, d, !1, l) : "6" == h ? this.insertNode(draftMsg, "6", g, "icon-draft", a, e, k, d, !1, l) : "4" == h ? this.insertNode(junkMsg, "4", g, "icon-junk", a, e, k, d, !1, l) : "3" == h ? this.insertNode(trashMsg, "3", g, "icon-trash", a, e, k, d, !1, l) : -1 == g.indexOf("@yahoo.com") && -1 == g.indexOf("@gmail.com") && this.insertNode(g, h, g, "icon-subfolder", a, e, k, d, !0, l) : f.push(this._model.bmailFolderStructure[e])
    } this.arrInspectNode = f; if (!this.dynamicLoad) for (c = this.getMaxDept(this._model.bmailFolderStructure), e = 0; e < c - 1;) {
        g = []; for (h = 0; h < d.length; h++) this.insertChildNode(d[h],
        f, e, g); e++; d = g
    } this.tree.draw(); d = { rootNode: this.getRoot() }; b || this.isSubTree || this.folderStructureChangeEvent.notify(d); this.isSubTree || (this.selectedNode && this.getNodeByProperty("labelElId", this.selectedNode.labelElId) ? this.addActiveStyle(this.getNodeByProperty("labelElId", this.selectedNode.labelElId)) : (this.addActiveStyle(this.getNodeByProperty("labelElId", "2")), this.selectedNode = this.getNodeByProperty("labelElId", "2")), this.handleNotifyFolderStructureChanged())
};
TreeView.prototype.dynamicLoadChildNode = function (a, b) { var c = TreeController.getInstance()._view.arrInspectNode; getSubfolderLabel(a.label); for (var d = 0; d < c.length; d++) { var f = c[d].visible; c[d].path.split("/"); if (c[d].parentId == a.labelElId) { var e = c[d].id, g = new YAHOO.widget.TextNode("<div class='htmlnodelabel'><a>" + c[d].folderName + "</a></div>", a, !1); g.labelStyle = "0" == f ? "icon-hiddenfolder" : "icon-subfolder"; g.labelElId = e } } b() };
TreeView.prototype.updateUnreadForNode = function (a, b, c, d) {
    var f = null; if (this.isSubTree) f = new YAHOO.widget.TextNode("<div class='htmlnodelabel'><a>" + a + "</a></div>", c, !1); else {
        b = FolderCache.getInstance().getFolder(b); if (!b) return new YAHOO.widget.TextNode("<div class='htmlnodelabel'><a>" + a + "</a></div>", c, !0); b = b.u; f = 0 < b ? d && "1" == d ? new YAHOO.widget.TextNode("<div class='htmlnodelabel'><b>" + a + " (" + b + ")</div>", c, !0) : new YAHOO.widget.TextNode("<div class='htmlnodelabel'><b>" + a + " (" + b + ")</div>", c, !1) : d && "1" ==
        d ? new YAHOO.widget.TextNode("<div class='htmlnodelabel'><a>" + a + "</a></div>", c, !0) : new YAHOO.widget.TextNode("<div class='htmlnodelabel'><a>" + a + "</a></div>", c, !1)
    } return f
};
TreeView.prototype.insertNode = function (a, b, c, d, f, e, g, h, l, k) {
    this.isSubTree || -1 == getIndexOf(this._model.linkExpires, b) ? (this.isSubTree && (g = !1), "1" == k ? (a = this.updateUnreadForNode(a, b, f, g), l && !this.isSubTree ? (d = Util.checkExistLinkShare(b), a.labelStyle = d ? d.shareFolder ? "icon-sharefolder" : "icon-publicfolder" : "icon-subfolder") : a.labelStyle = d, a.labelElId = b, a.nowrap = !0, h.push(a), this.isSubTree || (this._model.oTextNodeMap["ygtvcontentel" + a.index] = a, new DDList(TreeDragDrop.PREFIX_NODE_DRAG + a.index, "mainFolder"),
    this._model.folderIndex.push({ path: c, index: a.index, node: a }))) : this.displayHiddenFolder && (a = this.updateUnreadForNode(a, b, f, g), a.labelStyle = "icon-hiddenfolder", a.labelElId = b, h.push(a)), this.tree.draw()) : (c = Session.getInstance().getUser(), h = Session.getInstance().getAuthen(), folderAction(c, h, "delete", b))
};
TreeView.prototype.insertChildNode = function (a, b, c, d) {
    for (var f = getSubfolderLabel(a.label), e = 0; e < b.length; e++) {
        var g = b[e].visible, h = "0"; b[e].isOpen && (h = b[e].isOpen); var l = b[e].path, k = l.split("/"), m = b[e].parentId; 0 == c && ("2" == a.labelElId ? f = "inbox" : "5" == a.labelElId ? f = "sent" : "6" == a.labelElId ? f = "drafts" : "4" == a.labelElId ? f = "junk" : "3" == a.labelElId && (f = "trash")); var n = a.labelElId; "1" != g && !this.displayHiddenFolder || k.length != c + 2 || m != n || k[c].toUpperCase() != f.toUpperCase() || (m = b[e].id, h = this.updateUnreadForNode(k[c +
        1], m, a, h), h.labelStyle = this.displayHiddenFolder && "0" == g ? "icon-hiddenfolder" : "icon-subfolder", h.labelElId = m, h.nowrap = !0, this.isSubTree || "1" != g || (this._model.oTextNodeMap["ygtvcontentel" + h.index] = h, new DDList(TreeDragDrop.PREFIX_NODE_DRAG + h.index, a.labelElId), this._model.folderIndex.push({ path: l, index: h.index, node: h })), d.push(h), this.tree.draw())
    }
};
TreeView.prototype.handleNotifyFolderStructureChanged = function () { if ("TRUE" != this._model.bmailFolderStructureChanged) mailbox.getInstance().sendMailToTool(); else { var a = mailbox.getInstance().getFolderStructureByGetInfo(infoResponse, !0), a = TreeView.arr_diff(this._model.bmailFolderStructure, a); 0 < a.length && this.handleFolderChange(a) } };
TreeView.prototype.handleFolderChange = function (a) {
    var b = a.length, c = [], d = []; console.log("diff: " + a); for (var f = [], e = 0; e < b; e++) { var g = a[e].isInfo || !1; g && !a[e].perm && (g = !1); var h = a[e].id; g ? (g = a[e].path, -1 == g.indexOf("/") ? this.insertNode(g, h, g, "icon-subfolder", this.getRoot(), e, "1", c, !0, !0) : d.push(a[e])) : (g = this.tree.getNodeByProperty("labelElId", h), console.log("node: " + g), g && (this._model.removeFolderIndex(g), f.push(h))) } if (0 < f.length) for (b = 0; b < f.length; b++) (g = this.tree.getNodeByProperty("labelElId", f[b])) &&
    this.removeNode(g); f = this.getMaxDept(a); b = 0; if (0 < d.length) for (; b < f - 1;) { e = []; if (0 < c.length) for (h = 0; h < c.length; h++) this.insertChildNode(c[h], d, b, e); b++; c = e } this.draw(); c = { rootNode: this.getRoot() }; console.log("folderStructureChangeEvent"); this.folderStructureChangeEvent.notify(c); console.log("notifyFolderStructureChanged"); this.notifyFolderStructureChanged(a); mailbox.getInstance().sendMailToTool()
};
TreeView.arr_diff = function (a, b) { for (var c = [], d = [], f = 0; f < a.length; f++) c[a[f].id] = a[f]; for (f = 0; f < b.length; f++) c[b[f].id] ? delete c[b[f].id] : c[b[f].id] = b[f]; for (var e in c) d.push(c[e]); return d };
TreeView.prototype.notifyFolderStructureChanged = function (a) {
    var b = document.body.appendChild(document.createElement("div")); b.setAttribute("id", "moveDialog"); b.appendChild(document.createElement("div")).className = "hd"; var c = b.appendChild(document.createElement("div")); c.className = "bd"; c = c.appendChild(document.createElement("div")); c.setAttribute("id", "bodyMoveDialog"); c.style.overflow = "auto"; c.style.background = "none repeat scroll 0 0 #FFFFFF"; c.style.height = "300px"; c.style.width = "454px"; c.style.border =
    "1px solid #c0c0c0"; c.style.marginTop = "5px"; c.style.bgcolor = "#fdfcfa"; c = c.appendChild(document.createElement("div")); c.id = TreeController.subTreeId + share.INDEX; share.INDEX++; b.appendChild(document.createElement("div")).className = "ft"; var d; d = "<table cellpadding='4' cellspacing='4' style='width:400px;margin:20px'><tr><th>T\u00ean m\u1ee5c tin</th><th>Lo\u1ea1i thay \u0111\u1ed5i</>"; console.log("diff: " + a); var f = a.length; if (0 < f) for (var e = 0; e < f; e++) {
        var g = a[e], h = g.path, l = g.isInfo || !1; l && !g.perm && (l =
        !1); g = "X\u00f3a"; l && (g = "Th\u00eam m\u1edbi"); d += "<tr><td>" + h + "</td><td>" + g + "</td></tr>"
    } c.innerHTML = d + "</table>"; var k = this; a = new YAHOO.widget.SimpleDialog(b, { fixedcenter: !0, modal: !0, constraintoviewport: !0, close: !1, width: "480px", visible: !1 }); a.cfg.queueProperty("buttons", [{ text: chooseButtonMsg, handler: function () { this.cancel(); k._model.updateFolderStructureChanged() } }]); a.setHeader("<div class = 'rhd'></div><div class = 'rft'></div>C\u00e1c m\u1ee5c tin thay \u0111\u1ed5i"); a.render(document.body);
    a.bringToTop(); a.show(); a.focusEnter(handleOk); mailbox.getInstance().disableOutline()
};
TreeView.prototype.getTreeViewStructure = function (a, b) {
    if (a.children) {
        for (var c = a.children, d = c.length, f = 0; f < d; f++) {
            var e = this.getFirstParent(c[f]); if ((!e || e && "icon-contact" != e.labelStyle && "icon-calendar" != e.labelStyle && "icon-service" != e.labelStyle && "icon-facebook" != e.labelStyle) && "icon-contact" != c[f].labelStyle && "icon-calendar" != c[f].labelStyle && "icon-service" != c[f].labelStyle && "icon-facebook" != c[f].labelStyle) {
                var g = c[f].labelElId, h = getLocation(c[f]), l = new convertSpecialEntities; l.setInput(h); var h =
                l.convertToHTMLCode(), k = c[f].labelStyle, l = 1, m = "", m = c[f].parent && c[f].parent.labelElId ? c[f].parent.labelElId : "1"; "icon-hiddenFolder" == k && (l = 0); k = 0; c[f].expanded && (k = 1); if (e && ("icon-gtalk" == e.labelStyle || "icon-yahoo" == e.labelStyle) && (e = h.split("/"), h = "", h = e[0] + "@gmail.com", 1 < e.length)) for (var n = 1; n < e.length; n++) h += "/" + e[n]; b.push({ folderId: g, folderPath: h, parentId: m, hidden: l, link: 0, expanded: k })
            }
        } for (f = 0; f < d; f++) c[f].hasChildren && this.getTreeViewStructure(c[f], b)
    }
};
TreeView.prototype.getMaxDept = function (a) { for (var b = 0, c = 0; c < a.length; c++) { var d = a[c].path.split("/"); b < d.length && (b = d.length) } return b }; TreeView.prototype.addFacebookFolder = function (a) { mailbox.getInstance().isEgov || (a = new YAHOO.widget.TextNode("<div class='htmlnodelabel'><a>" + facebookWallMsg + "</a></div>", a, !0), a.labelElId = "facebookNode", a.labelStyle = "icon-facebook") };
TreeView.prototype.removeServiceNode = function () { mailbox.getInstance().isEgov || (this.removeNode(this.serviceNode), this.removeNode(this.contactNode), this.removeNode(this.calendarNode)) };
TreeView.prototype.addServiceNode = function (a) {
    mailbox.getInstance().isEgov || (this.serviceNode = new YAHOO.widget.TextNode("<div class='htmlnodelabel'><a>" + applicationMsg + "</a></div>", a, !0), this.serviceNode.labelElId = "serviceNode", this.serviceNode.labelStyle = "icon-service", this.hiddenServiceNode = new YAHOO.widget.TextNode("<div class='htmlnodelabel'><a>" + addressBookMsg + "</a></div>", this.serviceNode, !0), this.contactNode = new YAHOO.widget.TextNode("<div class='htmlnodelabel'><a>" + addressBookMsg + "</a></div>",
    a, !0), this.contactNode.labelElId = "contactNode", this.contactNode.labelStyle = "icon-contact", this.calendarNode = new YAHOO.widget.TextNode("<div class='htmlnodelabel'><a>" + calendarMsg + "</a></div>", a, !0), this.calendarNode.labelElId = "calendarNode", this.calendarNode.labelStyle = "icon-calendar", this.tree.draw(), this.serviceNode.hideChildren(), this.tree.draw(), YAHOO.util.Dom.setStyle(this.hiddenServiceNode.getElId(), "display", "none"), "TRUE" === mailbox.getInstance().serviceExpandState ? this.serviceNode && this.serviceNode.expand() :
    this.serviceNode && this.serviceNode.collapse())
};
TreeView.prototype.handleSelectFolder = function (a, b) {
    this.oContextMenu && !this.contextMenuClick && this.oContextMenu.hide(); this.contextMenuClick && (this.contextMenuClick = !1); this.isSubTree || 0 == tabView.get("activeIndex") || tabView.set("activeIndex", 0); this.selectedNode || (this.selectedNode = this.getNodeByProperty("labelElId", "2")); if (a != this.selectedNode) if (dataTable.scrollTop = 0, EmailDragDrop.getInstance().reset(), -1 != getIndexOf(this._model.linkExpires, a.labelElId)) {
        var c = this; new ConfirmBox(messageBoxMsg,
        noFolderRoleMsg, function () {
            var b = Session.getInstance().getUser(), e = Session.getInstance().getAuthen(), d = a.labelElId, b = folderAction(b, e, "delete", d); b.Body.FolderActionResponse && (b.Body.FolderActionResponse.action && b.Body.FolderActionResponse.action.id ? d == b.Body.FolderActionResponse.action.id ? (c.removeNode(a), c._model.removeFolderIndex(a), c.tree.draw(), d = { rootNode: c.getRoot() }, c.folderStructureChangeEvent.notify(d)) : new MessageBox(messageBoxMsg, "B\u1ea1n kh\u00f4ng c\u00f3 quy\u1ec1n x\u00f3a th\u01b0 m\u1ee5c n\u00e0y!") :
            new MessageBox(messageBoxMsg, "B\u1ea1n kh\u00f4ng c\u00f3 quy\u1ec1n x\u00f3a th\u01b0 m\u1ee5c n\u00e0y!"))
        })
    } else if (this.addActiveStyle(a), currentUser.scrollToGetMail || paging.getInstance().reset(), !this.isSubTree) {
        toolbar.classifyButton && (toolbar.resetState(), toolbar.classifyButton.set("label", allListboxMsg)); YAHOO.util.Dom.setStyle(document.body, "cursor", "wait"); "icon-send" == a.labelStyle ? (dataTable.table.hideColumn("from"), dataTable.table.showColumn("to")) : (dataTable.table.hideColumn("to"), dataTable.table.showColumn("from"));
        FolderCache.getInstance().set(a.labelElId, "lastClickTime", (new Date).getTime()); this._model.handleLastCacheData(this.selectedNode); currentUser.scrollToGetMail && ("MAX" != FolderCache.getInstance().get(a.labelElId, "lastIndex") || mailbox.getInstance().listMailStorage || FolderCache.getInstance().set(a.labelElId, "lastIndex", 0)); this.selectedNode = a; var d = { node: a }; b && (d.mailId = b); c = this; window.setTimeout(function () { c.selectFolderEvent.notify(d) }, 300)
    }
};
TreeView.prototype.initCalendar = function () {
    var a = getindexTabByLabel(calendarMsg); -1 != a && tabView.set("activeIndex", a); if (-1 == a) {
        a = getindexNewTab(txtSoan); tabView.removeTab(tabView.getTab(a)); var b = "Microsoft Internet Explorer" == navigator.appName ? new Tab("calendarTab", "<span id='calendarImage' style='margin-left:-45px;margin-top: 10px;'></span><span class = 'calendarTabLabel' style='padding-top: 10px;position: absolute;font-style:normal;margin-left:-15px;color :#6FAEEE;font-size:12px;font-family:Tahoma'>" +
        calendarMsg + "</span><span class='closeTabIE' id='closecalendartab'><img src='icon1/Closeinactive.png' class ='closeSubTabIE' id='closecalendartab' name='Calendar'>", "", !0) : new Tab("calendarTab", '<span id="calendarImage" style=""></span><span style="margin-top:5px;"><span style="position:absolute;left:20px;margin-top: 7px;color: #6FAEEE;">' + calendarMsg + '</span><div class="closeTab" id="closecalendartab"><img  name="Calendar" style="position:absolute;right:4px;bottom:5px;" src="icon1/Closeinactive.png"></span></div>',
        "", !0); b.setDataSrc("calendar/calendar.html"); b.init(); YAHOO.plugin.Dispatcher.delegate(b.tab, tabView); YAHOO.util.Event.on("closecalendartab", "click", function () { $("#CalendarEnterpriseLeft").empty(); tabView.removeTab(b.tab); tabView.set("activeIndex", 0) }); a = new YAHOO.widget.Tab({ label: txtSoan, id: "composeMailTab" }); tabView.addTab(a); $(".closeTabIE").mouseover(function () { $(this).css("background", "url('css/giaodien/bgclose.png') no-repeat") }); $(".closeTabIE").mouseout(function () {
            $(this).css("background",
            "none")
        }); tabManager.openComposeTab()
    } YAHOO.util.Dom.setStyle(document.body, "cursor", "default")
}; TreeView.prototype.initContact = function () { document.getElementById("treeDiv").style.display = "none"; var a = new ContactModel, b = new ContactView(a); (new ContactController(a, b)).init(); YAHOO.util.Dom.setStyle(document.body, "cursor", "default") };
TreeView.prototype.initFacebook = function () {
    var a = this, b = "Microsoft Internet Explorer" == navigator.appName ? new YAHOO.widget.Tab({
        label: "<span class = 'calendarTabLabel' style='padding-top: 10px;position: absolute;font-style:normal;margin-left:-15px;color :#6FAEEE;font-size:12px;font-family:Tahoma'>" + facebookMsg + "</span><span class='closeTabIE' id='closefacebookTab'><img src='icon1/Closeinactive.png' class ='closeSubTabIE' id='closecalendartab' name='Facebook'>", content: "", id: "facebookTab", active: !0,
        cacheData: !0, dataSrc: "js/facebook/dsFacebook.html"
    }) : new YAHOO.widget.Tab({ label: '<span style="position:absolute;left:20px;margin-top: 7px;color: #6FAEEE;">' + facebookMsg + '</span><div class="closeTab" id="closefacebookTab"><img  name="Facebook" style="position:absolute;right:4px;bottom:4px;" src="icon1/Closeinactive.png"></span></div>', content: "", id: "facebookTab", active: !0, cacheData: !0, dataSrc: "js/facebook/dsFacebook.html" }); tabView._tabManager.addTab(b, 1); window.setTimeout(function () { initFBvalue() },
    300); YAHOO.util.Event.on("closefacebookTab", "click", function () { a.addActiveStyle(a.selectedNode); 9 > browserVersion() && "Microsoft Internet Explorer" == navigator.appName ? (tabView.removeTab(b), tabView.set("activeIndex", 0)) : ($("#facebookTab").addClass("close"), $("#facebookNode .activeNode").removeClass("activeNode"), window.setTimeout(function () { $("#inbox").click() }, 10)) }); $(".closeTabIE").mouseover(function () { $(this).css("background", "url('css/giaodien/bgclose.png') no-repeat") }); $(".closeTabIE").mouseout(function () {
        $(this).css("background",
        "none")
    })
};
TreeView.prototype.addActiveStyle = function (a) {
    var b = "activeNode"; this.isSubTree && (b = "activeSubNode"); $("." + b).each(function () { $(this).removeClass(b); $(this).find("b").removeAttr("style"); $(this).find("a").removeAttr("style"); "Microsoft Internet Explorer" == navigator.appName && $(this).removeAttr("style") }); if (a && (a = this.getNodeByProperty("labelElId", a.labelElId))) {
        var c = a.getElId(); $("#" + c + " .htmlnodelabel").addClass(b); a.hasChildren() && ($("#" + a.getChildrenElId() + " .htmlnodelabel").removeClass(b), $("#" +
        a.getChildrenElId() + " .htmlnodelabel").find("b").removeAttr("style"), $("#" + a.getChildrenElId() + " .htmlnodelabel").find("a").removeAttr("style"))
    } $("." + b).each(function () { $(this).find("b").css("color", "#fff"); $(this).find("a").css("color", "#fff") }); this.isSubTree && (this.selectedNode = a)
}; TreeView.prototype.removeNode = function (a) { this.tree.removeNode(a) };
TreeView.prototype.postToThisFolder = function () {
    this.postToThisFolderEvent.notify()
}; TreeView.prototype.syncFolder = function (a) { this.syncFolderEvent.notify({ node: a }) };
TreeView.prototype.createNewFolder = function (a) { this.createNewFolderEvent.notify({ node: a }) }; TreeView.prototype.deleteFolder = function (a) { this.deleteFolderEvent.notify({ node: a }) }; TreeView.prototype.moveFolder = function (a) { this.moveFolderEvent.notify({ node: a }) }; TreeView.prototype.renameFolder = function (a) { this.renameFolderEvent.notify({ node: a }) }; TreeView.prototype.createPublicFolder = function (a) { this.createPublicFolderEvent.notify({ node: a }) }; TreeView.prototype.createShareFolder = function (a) { this.createShareFolderEvent.notify({ node: a }) };
TreeView.prototype.editFolder = function (a, b) { this.editFolderEvent.notify({ node: a, flag: b }) }; TreeView.prototype.markAllReadFolder = function (a) { this.markAllReadFolderEvent.notify({ node: a }) }; TreeView.prototype.emptyFolder = function (a) { this.emptyFolderEvent.notify({ node: a }) }; TreeView.prototype.manageFolders = function () { this.manageFoldersEvent.notify() };
TreeView.prototype.createContextMenu = function () {
    var a = this, b = []; b.push([{ text: "<img src='treeTable/guivaoday.png' style='margin-bottom:-2px;margin-top: 2px;margin-right:10px'>" + postFolderMsg, onclick: { fn: function () { a.postToThisFolder(a.oCurrentTextNode) } }, disabled: !1 }, { text: "<img src='treeTable/synchonize.png' style='margin-bottom:-2px;margin-top: 2px;margin-right:10px'>" + syncFolderMsg, onclick: { fn: function () { a.syncFolder(a.oCurrentTextNode) } }, disabled: !1 }]); b.push([{
        text: "<img src='treeTable/taomoimuctin.png' style='margin-bottom:-2px;margin-top: 2px;margin-right:10px'>" +
        createFolderMsg, onclick: { fn: function () { a.createNewFolder(a.oCurrentTextNode) } }, disabled: !1
    }, { text: "<img src='treeTable/xoamuctin.png' style='margin-bottom:-2px;margin-top: 2px;margin-right:10px'>" + deleteFolderMsg, onclick: { fn: function () { a.deleteFolder(a.oCurrentTextNode) } }, disabled: !1 }, { text: "<img src='menuTable/dichuyen.png' style='margin-bottom:-2px;margin-top: 2px;margin-right:10px'>" + moveFolderMsg, onclick: { fn: function () { a.moveFolder(a.oCurrentTextNode) } }, disabled: !1 }, {
        text: "<img src='treeTable/doitenmuctin.png' style='margin-bottom:-2px;margin-top: 2px;margin-right:10px'>" +
        renameFolderMsg, onclick: { fn: function () { a.renameFolder(a.oCurrentTextNode) } }, disabled: !1
    }]); currentUser.canCreatePublicFolder ? b.push([{
        text: "<img src='treeTable/publicFolder.png' style='margin-bottom:-2px;margin-top: 2px;margin-right:10px'>" + publicFolderMsg, submenu: {
            id: "subPublicFolder_" + TreeView.TREE_ID, itemdata: [[{ text: "<img src='treeTable/publicFolder.png' style='margin-bottom:-2px;margin-top: 3px;margin-right: 7px;'>" + createPublicFolderMsg, onclick: { fn: function () { a.createPublicFolder(a.oCurrentTextNode) } } }],
            [{ text: "<img src='treeTable/chinhsuamuctin.png' style='margin-bottom:-2px;margin-top: 3px;margin-right: 7px;'>" + editPublicFolderMsg, onclick: { fn: function () { a.editFolder(a.oCurrentTextNode, "publicfolder") } } }]]
        }, disabled: !1
    }, {
        text: "<img src='treeTable/chiasemuctin.png' style='margin-bottom:-2px;margin-top: 2px;margin-right:10px'>" + shareFolderMsg, submenu: {
            id: "subShareFolder_" + TreeView.TREE_ID, itemdata: [[{
                text: "<img src='treeTable/publicFolder.png' style='margin-bottom:-2px;margin-top: 3px;margin-right: 7px;'>" +
                createShareFolderMsg, onclick: { fn: function () { a.createShareFolder(a.oCurrentTextNode) } }
            }], [{ text: "<img src='treeTable/chinhsuamuctin.png' style='margin-bottom:-2px;margin-top: 3px;margin-right: 7px;'>" + editShareFolderMsg, onclick: { fn: function () { a.editFolder(a.oCurrentTextNode, "sharefolder") } } }]]
        }, disabled: !1
    }]) : b.push([{
        text: "<img src='treeTable/chiasemuctin.png' style='margin-bottom:-2px;margin-top: 2px;margin-right:10px'>" + shareFolderMsg, submenu: {
            id: "subShareFolder_" + TreeView.TREE_ID, itemdata: [[{
                text: "<img src='treeTable/publicFolder.png' style='margin-bottom:-2px;margin-top: 3px;margin-right: 7px;'>" +
                createShareFolderMsg, onclick: { fn: function () { a.createShareFolder(a.oCurrentTextNode) } }
            }], [{ text: "<img src='treeTable/chinhsuamuctin.png' style='margin-bottom:-2px;margin-top: 3px;margin-right: 7px;'>" + editShareFolderMsg, onclick: { fn: function () { a.editFolder(a.oCurrentTextNode, "sharefolder") } } }]]
        }, disabled: !1
    }]); b.push([{
        text: "<img src='treeTable/tatcadadoc.png' style='margin-bottom:-2px;margin-top: 2px;margin-right:10px'>" + markAllReadMsg, onclick: { fn: function () { a.markAllReadFolder(a.oCurrentTextNode) } },
        disabled: !1
    }, { text: "<img src='treeTable/xoa.png' style='margin-bottom:-2px;margin-top: 2px;margin-right:10px'>" + emptyFolderMsg, onclick: { fn: function () { a.emptyFolder(a.oCurrentTextNode) } }, disabled: !1 }], [{ text: "<img src='treeTable/quanlymuctin.png' style='margin-bottom:-2px;margin-top: 2px;margin-right:10px'>" + managmentFolderMsg, onclick: { fn: function () { a.manageFolders(a.oCurrentTextNode) } }, disabled: !1 }]); share.INDEX++; this.oContextMenu = new YAHOO.widget.ContextMenu(TreeView.CONTEXT_MENU_ID + share.INDEX,
    { trigger: TreeView.TREE_ID, width: "230px", lazyload: !0, itemdata: b }); this.oContextMenu.show(); this.oContextMenu.hide(); "tool" == mailbox.getInstance().client && this.oContextMenu.subscribe("click", function (b, d, f) {
        (b = d[1]) && 3 == b.groupIndex && 2 == b.index && (b = a.oCurrentTextNode.labelElId, -1 != a.oContextMenu.getItem(2, 3).cfg.getProperty("text").indexOf("B\u1ecf theo d\u00f5i m\u1ee5c tin") ? (console.log("B\u1ecf theo d\u00f5i m\u1ee5c tin"), Tool.getInstance().setFolderNotify(b, "1")) : (console.log("theo d\u00f5i m\u1ee5c tin"),
        Tool.getInstance().setFolderNotify(b, "0")))
    }); $("#subShareFolder_" + a.id).addClass("subShareFolder"); this.oContextMenu.subscribe("triggerContextMenu", function (b) { mailbox.getInstance().disableOutline(); a.oCurrentTextNode = a.getNodeByElement(this.contextEventTarget); a.oCurrentTextNode || this.cancel(); "serviceNode" != a.oCurrentTextNode.labelElId && "contactNode" != a.oCurrentTextNode.labelElId && "calendarNode" != a.oCurrentTextNode.labelElId || this.cancel() }); this.oContextMenu.subscribe("show", function () {
        a.contextMenuClick =
        !0; window.setTimeout(function () { a.handleSelectFolder(a.oCurrentTextNode) }, 50)
    }); YAHOO.util.Event.on(document, "click", function (b) { b = YAHOO.util.Event.getTarget(b); var d = a.oContextMenu.element; b == d || YAHOO.util.Dom.isAncestor(d, b) || a.oContextMenu.hide() }); $(window).bind("blur", function () { a.oContextMenu.hide() }); this.oContextMenu.subscribe("beforeShow", function (b, d) {
        var f = "317px", f = currentUser.canCreatePublicFolder ? "317px" : "293px", e = a.oCurrentTextNode.labelElId; if ("tool" == mailbox.getInstance().client) {
            a.oContextMenu.getItem(2,
            3) && a.oContextMenu.removeItem(2, 3); var g = getLocation(a.oCurrentTextNode); if (-1 == in_array(g, Tool.getInstance().notifyFolder) || 1 == currentUser.lanhdao) console.log("Tool.getInstance().getFolderNotify(folderId): " + Tool.getInstance().getFolderNotify(e)), Tool.getInstance().getFolderNotify(e) ? a.oContextMenu.addItem({ text: "<img src='menuTable/NotifyFolder.ico' style='margin-bottom:-2px;margin-top: 2px;margin-right:7px;height:16px;height:16px;'> Theo d\u00f5i m\u1ee5c tin" }, 3) : a.oContextMenu.addItem({ text: "<img src='menuTable/NotifyFolder.ico' style='margin-bottom:-2px;margin-top: 2px;margin-right:7px;height:16px;height:16px;'> B\u1ecf theo d\u00f5i m\u1ee5c tin" },
            3), f = 1 == currentUser.canCreatePublicFolder ? "342px" : "318px"
        } a.oContextMenu.cfg.setProperty("height", f); $(".menuDrop,.bong,.menuDropChild").css("display", "none"); a.addActiveStyle(a.oCurrentTextNode); a.oCurrentTextNode.toggleHighlight(); f = a.getFirstParent(a.oCurrentTextNode); f || (f = a.oCurrentTextNode); if ("icon-gtalk" == f.labelStyle || "icon-yahoo" == f.labelStyle) {
            for (e = 0; e < this.itemData[1].length; e++) this.oContextMenu.getItem(e, 1).cfg.setProperty("disabled", !0); for (e = 0; e < this.itemData[2].length; e++) this.oContextMenu.getItem(e,
            2).cfg.setProperty("disabled", !0); for (e = 0; e < this.itemData[4].length; e++) this.oContextMenu.getItem(e, 4).cfg.setProperty("disabled", !0); if ("icon-gtalk" == a.oCurrentTextNode.labelStyle || "icon-yahoo" == a.oCurrentTextNode.labelStyle) this.oContextMenu.getItem(1, 1).cfg.setProperty("disabled", !1), a.addDeleteAccountItem(this.oContextMenu)
        } else if ("icon-sharefolder" == f.labelStyle || "icon-publicfolder" == f.labelStyle) {
            for (e = 0; e < this.itemData[0].length; e++) this.oContextMenu.getItem(e, 0).cfg.setProperty("disabled",
            !1); for (e = 0; e < this.itemData[1].length; e++) this.oContextMenu.getItem(e, 1).cfg.setProperty("disabled", !1); for (e = 0; e < this.itemData[2].length; e++) this.oContextMenu.getItem(e, 2).cfg.setProperty("disabled", !1); for (e = 0; e < this.itemData[3].length; e++) this.oContextMenu.getItem(e, 3).cfg.setProperty("disabled", !1); for (e = 0; e < this.itemData[4].length; e++) this.oContextMenu.getItem(e, 4).cfg.setProperty("disabled", !1); a.oContextMenu.getItem(0, 2).cfg.setProperty("disabled", !0); a.oContextMenu.getItem(1, 2).cfg.setProperty("disabled",
            !0); "icon-publicfolder" == f.labelStyle && a.oContextMenu.getItem(1, 1).cfg.setProperty("disabled", !0)
        } else if ("icon-contactFolder" == a.oCurrentTextNode.labelStyle || "facebookNode" == a.oCurrentTextNode.labelElId || "serviceNode" == a.oCurrentTextNode.labelElId || "contactNode" == a.oCurrentTextNode.labelElId || "calendarNode" == a.oCurrentTextNode.labelElId) this.cancel(); else if ("2" == a.oCurrentTextNode.labelElId || "5" == a.oCurrentTextNode.labelElId || "6" == a.oCurrentTextNode.labelElId || "4" == a.oCurrentTextNode.labelElId ||
        "3" == a.oCurrentTextNode.labelElId) {
            for (e = 0; e < this.itemData[1].length; e++) a.oContextMenu.getItem(e, 1).cfg.setProperty("disabled", !0); a.oContextMenu.getItem(0, 1).cfg.setProperty("disabled", !1); for (e = 0; e < this.itemData[2].length; e++) a.oContextMenu.getItem(e, 2).cfg.setProperty("disabled", !1); "6" != a.oCurrentTextNode.labelElId && "4" != a.oCurrentTextNode.labelElId && "3" != a.oCurrentTextNode.labelElId || a.oContextMenu.getItem(0, 1).cfg.setProperty("disabled", !0); a.oContextMenu.getItem(0, 3).cfg.setProperty("disabled",
            !1); a.oContextMenu.getItem(1, 3).cfg.setProperty("disabled", !1); for (e = 0; e < this.itemData[4].length; e++) a.oContextMenu.getItem(e, 4).cfg.setProperty("disabled", !1)
        } else {
            Util.checkExistLinkId(a.oCurrentTextNode.labelElId, linkFolder) && a.oContextMenu.getItem(1, 1).cfg.setProperty("disabled", !0); for (e = 0; e < this.itemData[0].length; e++) a.oContextMenu.getItem(e, 0).cfg.setProperty("disabled", !1); for (e = 0; e < this.itemData[1].length; e++) a.oContextMenu.getItem(e, 1).cfg.setProperty("disabled", !1); for (e = 0; e < this.itemData[2].length; e++) a.oContextMenu.getItem(e,
            2).cfg.setProperty("disabled", !1); for (e = 0; e < this.itemData[2].length; e++) a.oContextMenu.getItem(e, 4).cfg.setProperty("disabled", !1)
        }
    })
}; TreeView.prototype.redrawTreeView = function (a) { this._model.folderIndex = []; this._model.oTextNodeMap = []; this.expandClick = !1; this.tree.removeChildren(this.tree.getRoot()); this.draw(); this.buildChildNode(this.tree.getRoot(), !0); this.draw(); this._model.sync || a || (this._model.sync = !0, a = { rootNode: this.getRoot() }, this.folderStructureChangeEvent.notify(a)) };
TreeView.prototype.removeChildren = function (a) { this.expandClick = !1; this.tree.removeChildren(a, !0); this.expandClick = !0 };
TreeView.prototype.addFolderMailExternal = function () { var a = currentUser.getMailTransport(), b = currentUser.getChatTransport(); if (0 < a.length && 0 == b.length) { var c = this; YAHOO.util.Get.script(["js/MailExternal/mailExternal.js.zgz"], { onFailure: function () { }, onSuccess: function () { MailExternalManager.getInstance().setTree(c); MailExternalManager.getInstance().getAccountExternal(getInfoResponse); MailExternalManager.getInstance().addFolderExternal() } }) } };
TreeView.prototype.addDeleteAccountItem = function (a) { if (0 < currentUser.getMailTransport().length) { var b = this; a.getItem(1, 4) && a.removeItem(1, 4); a.addItem({ text: "<img src='treeTable/xoamuctin.png' style='margin-bottom:-2px;margin-top: 2px;margin-right:7px;'> X\u00f3a t\u00e0i kho\u1ea3n li\u00ean th\u00f4ng", onclick: { fn: function () { b.deleteAccount(b.oCurrentTextNode) } } }, 4); var c = "", c = 1 == currentUser.canCreatePublicFolder ? "347px" : "323px"; a.cfg.setProperty("height", c) } };
TreeView.prototype.deleteAccount = function (a) { var b = a.labelStyle, c = ""; "icon-gtalk" == b || "icon-gmail" == b ? c = "gmail" : "icon-yahoo" == b && (c = "yahoo"); b = a.getElId(); a = $("#" + b).offset().left; b = $("#" + b).offset().top; new ConfirmBox(deleteAccountMsg + c, cfDeleteAccountMsg + c, function () { MailExternalManager.getInstance().getMailExternal(c).handleDeleteAccount() }, function () { }, chooseButtonMsg, !1, "315px", a, b, !0) }; TreeView.prototype.getRoot = function () { return this.tree.getRoot() }; TreeView.prototype.getSelectedNode = function () { return this.selectedNode };
TreeView.prototype.getNodeByElement = function (a) { return this.tree.getNodeByElement(a) }; TreeView.prototype.getFirstParent = function (a) { var b = null; if (a) for (; a && a.parent != this.getRoot() ;) a = b = a.parent; return b }; TreeView.prototype.getNodeByProperty = function (a, b) { return this.tree.getNodeByProperty(a, b) }; TreeView.prototype.draw = function () { this.tree.draw(); this.addActiveStyle(this.getSelectedNode()) };