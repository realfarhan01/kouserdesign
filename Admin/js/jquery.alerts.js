// jQuery Alert Dialogs Plugin
//
// Version 1.1
//
// Cory S.N. LaViska
// A Beautiful Site (http://abeautifulsite.net/)
// 14 May 2009
//
// Website: http://abeautifulsite.net/blog/2008/12/jquery-alert-dialogs/
//
// Usage:
//		jAlert( message, [title, callback] )
//		jConfirm( message, [title, callback] )
//		jPrompt( message, [value, title, callback] )
// 
// History:
//
//		1.00 - Released (29 December 2008)
//
//		1.01 - Fixed bug where unbinding would destroy all resize events
//
// License:
// 
// This plugin is dual-licensed under the GNU General Public License and the MIT License and
// is copyright 2008 A Beautiful Site, LLC. 
//
(function ($) {
    $.alerts = {
        verticalOffset: -75, horizontalOffset: 0, repositionOnResize: !0, overlayOpacity: .01, overlayColor: '#FFF', draggable: !0, okButton: '&nbsp;OK&nbsp;', closeOnClick: !0, cancelButton: '&nbsp;Cancel&nbsp;', dialogClass: null, alert: function (message, title, callback) { if (title == null) title = 'Alert'; $.alerts._show(title, message, null, 'alert', function (result) { if (callback) callback(result) }) }, confirm: function (message, title, callback) { if (title == null) title = 'Confirm'; $.alerts._show(title, message, null, 'confirm', function (result) { if (callback) callback(result) }) }, prompt: function (message, value, title, callback) { if (title == null) title = 'Prompt'; $.alerts._show(title, message, value, 'prompt', function (result) { if (callback) callback(result) }) }, _show: function (title, msg, value, type, callback) {
            $.alerts._hide(); $.alerts._overlay('show'); $("BODY").append('<div id="popup_container">' + '<h1 id="popup_title"></h1>' + '<div id="popup_content">' + '<div id="popup_message"></div>' + '</div>' + '</div>'); if ($.alerts.dialogClass) $("#popup_container").addClass($.alerts.dialogClass); var pos = ($.browser.msie && parseInt($.browser.version) <= 6) ? 'absolute' : 'fixed'; $("#popup_container").css({ position: pos, zIndex: 99999, padding: 0, margin: 0 }); $("#popup_title").text(title); $("#popup_content").addClass(type); $("#popup_message").text(msg); $("#popup_message").html($("#popup_message").text().replace(/\n/g, '<br />')); $("#popup_container").css({ minWidth: $("#popup_container").outerWidth(), maxWidth: $("#popup_container").outerWidth() }); $.alerts._reposition(); $.alerts._maintainPosition(!0); switch (type) { case 'alert': $("#popup_message").after('<div id="popup_panel"><input type="button" style="display:none;" value="' + $.alerts.okButton + '" id="popup_ok" /></div>'); $("#popup_ok").click(function () { $.alerts._hide(); callback(!0) }); $("#popup_ok").focus().keypress(function (e) { if (e.keyCode == 13 || e.keyCode == 27) $("#popup_ok").trigger('click') }); break; case 'confirm': $("#popup_message").after('<div id="popup_panel"><input type="button" value="' + $.alerts.okButton + '" id="popup_ok" /> <input type="button" value="' + $.alerts.cancelButton + '" id="popup_cancel" /></div>'); $("#popup_ok").click(function () { $.alerts._hide(); if (callback) callback(!0) }); $("#popup_cancel").click(function () { $.alerts._hide(); if (callback) callback(!1) }); $("#popup_ok").focus(); $("#popup_ok, #popup_cancel").keypress(function (e) { if (e.keyCode == 13) $("#popup_ok").trigger('click'); if (e.keyCode == 27) $("#popup_cancel").trigger('click') }); break; case 'prompt': $("#popup_message").append('<br /><input type="text" size="30" id="popup_prompt" />').after('<div id="popup_panel"><input type="button" value="' + $.alerts.okButton + '" id="popup_ok" /> <input type="button" value="' + $.alerts.cancelButton + '" id="popup_cancel" /></div>'); $("#popup_prompt").width($("#popup_message").width()); $("#popup_ok").click(function () { var val = $("#popup_prompt").val(); $.alerts._hide(); if (callback) callback(val) }); $("#popup_cancel").click(function () { $.alerts._hide(); if (callback) callback(null) }); $("#popup_prompt, #popup_ok, #popup_cancel").keypress(function (e) { if (e.keyCode == 13) $("#popup_ok").trigger('click'); if (e.keyCode == 27) $("#popup_cancel").trigger('click') }); if (value) $("#popup_prompt").val(value); $("#popup_prompt").focus().select(); break }
            if ($.alerts.draggable) { try { $("#popup_container").draggable({ handle: $("#popup_title") }); $("#popup_title").css({ cursor: 'move' }) } catch (e) { } }
        }, _hide: function () { $("#popup_container").remove(); $.alerts._overlay('hide'); $.alerts._maintainPosition(!1) }, _overlay: function (status) { switch (status) { case 'show': $.alerts._overlay('hide'); $("BODY").append('<div id="popup_overlay"></div>'); $("#popup_overlay").css({ position: 'absolute', zIndex: 99998, top: '0px', left: '0px', width: '100%', height: $(document).height(), background: $.alerts.overlayColor, opacity: $.alerts.overlayOpacity }); break; case 'hide': $("#popup_overlay").remove(); break } }, _reposition: function () { var top = (($(window).height() / 2) - ($("#popup_container").outerHeight() / 2)) + $.alerts.verticalOffset; var left = (($(window).width() / 2) - ($("#popup_container").outerWidth() / 2)) + $.alerts.horizontalOffset; if (top < 0) top = 0; if (left < 0) left = 0; if ($.browser.msie && parseInt($.browser.version) <= 6) top = top + $(window).scrollTop(); $("#popup_container").css({ top: top + 'px', left: left + 'px' }); $("#popup_overlay").height($(document).height()) }, _maintainPosition: function (status) { if ($.alerts.repositionOnResize) { switch (status) { case true: $(window).bind('resize', $.alerts._reposition); break; case false: $(window).unbind('resize', $.alerts._reposition); break } } }
    }
    jAlert = function (message, title, callback) { $.alerts.alert(message, title, callback) }
    jConfirm = function (message, title, callback) { $.alerts.confirm(message, title, callback) }; jPrompt = function (message, value, title, callback) { $.alerts.prompt(message, value, title, callback) }
})(jQuery)