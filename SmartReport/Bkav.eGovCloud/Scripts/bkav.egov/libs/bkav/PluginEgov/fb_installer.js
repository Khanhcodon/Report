/*****************************************************************\
Firebreath JS installer and instantiation script
Original Author: Richard Bateman (taxilian)

Created:    Dec 9, 2009
License:    Dual license model; choose one of two:
            New BSD License
            http://www.opensource.org/licenses/bsd-license.php
            - or -
            GNU Lesser General Public License, version 2.1
            http://www.gnu.org/licenses/lgpl-2.1.html

Copyright 2009 PacketPass, Inc and the Firebreath development team
\*****************************************************************/

/*********************************************\
 This package defines shared FB functionality.
 Individual plugin data is generated at the end
\*********************************************/
if (typeof FireBreath === 'undefined') {
    FireBreath = {};
    FireBreath.pluginDefs = {};

    FireBreath.$ = function (el) {
        if (typeof (el) == "string") {
            return document.getElementById(el);
        } else {
            return el;
        }
    };

    // returns:
    //	- null: no plugin definition found in FireBreath.pluginDefs
    //	- false: plugin not installed
    //	- true: plugin exists but no version could be obtained
    //	- <NPAPI>:version number from np DLL file-name
    //	- <IE>:version property returned by instanced plugin object
    FireBreath.isPluginInstalled = function (pluginName) {
        //check if plugin exists
        if (!FireBreath.pluginDefs[pluginName]) {
            return null;
        }

        if (window.ActiveXObject || (!window.ActiveXObject && Object.hasOwnProperty.call(window, "ActiveXObject"))) {
            // We're running IE
            return FireBreath._isIEPluginInstalled(pluginName);
        } else if (navigator.plugins) {
            // We're running something else
            return FireBreath._isNpapiPluginInstalled(pluginName);
        }
    };

    FireBreath._isIEPluginInstalled = function (pluginName) {
        var axname = FireBreath.pluginDefs[pluginName].activeXName;

        //check if plugin exists
        var plugin;
        try {
            plugin = new ActiveXObject(axname);
        } catch (e) {
            return null;
        }

        var version = false;

        if (plugin) {
            try {
                version = plugin.version;
            } catch (e) {
                version = true; // Installed, unknown version
            }
        }
        return version;
    };

    FireBreath._isNpapiPluginInstalled = function (pluginName) {
        var plugin = null;
        for (var i = 0; i < navigator.plugins.length; i++) {
            if (navigator.plugins[i].name === pluginName) {
                plugin = navigator.plugins[i];
                break;
            }
        }
        if (plugin) {
            var re = /([0-9.]+)\.dll/; // look for the version at the end of the filename, before dll

            // Get the filename
            var filename = plugin.filename;
            // Search for the version
            var fnd = re.exec(filename);
            if (fnd === null) { // no version found
                return true; // plugin installed, unknown version
            } else {
                return fnd[1]; // plugin installed, returning version
            }
        }

        return false;
    };

    FireBreath.waitId = -1;
    FireBreath.waitForInstall = function (pluginName, callback) {
        if (FireBreath.isPluginInstalled(pluginName)) {
            callback(FireBreath.isPluginInstalled(pluginName));
            FireBreath.waitId = -1;
        } else {
            FireBreath.waitId = setTimeout(
                    function () { FireBreath.waitForInstall(pluginName, callback); },
                    FireBreath.pollInterval);
        }
    };

    FireBreath.abortWait = function () {
        if (FireBreath.waitId != -1) {
            clearInterval(FBPluginMgr.waitId);
            FireBreath.waitId = -1;
        }
    };

    FireBreath.injectPlugin = function (dest, pluginName, id, callback, params) {
        var info = FireBreath.pluginDefs[pluginName];
        if (typeof (params) == "undefined") {
            params = {};
        }
        __pluginCB = callback;
        params["onload"] = "__pluginCB";
        var html = '<object id="' + id + '" type="' + info.mimeType + '" width="0px" height="0px">';
        for (paramName in params) {
            html += '<param name="' + paramName + '" value="' + params[paramName] + '" />';
        }
        html += '</object>';
        //return html;
        dest.innerHTML = html;
    };

    FireBreath.addListener = (document.attachEvent) ?
        function (obj, type, handler) {
            obj.attachEvent("on" + type, handler);
        } : function (obj, type, handler) {
            obj.addEventListener(type, handler, false);
        };

    FireBreath.ifInstalled = function (pluginName, yes, no) {
        if (FBPluginMgr.isPluginInstalled(pluginName))
            yes(FBPluginMgr.isPluginInstalled(pluginName));
        else
            no();
    };
}


/*********************************************\
 This is the only part of the file which changes
 for each plugin, when generated by CMake.
 
 Here the data for all the plugins is defined
 - To support multiple plugins with the same
   install script, add additional entries
 - To support one plugin with multiple mime-
   types, pretend that it's a second plugin =]
\*********************************************/
FireBreath.pluginDefs.eOfficePlus = {
    "name": "eOfficePlus ",
    "mimeType": "application/x-eofficeplus",
    "activeXName": "Bkav.eOfficePlus",
    "guid": "{0a94732f-9d82-5fe3-90a1-364fd84dc343}",
    "installURL": {
        "win": "/Download/EOfficePlusPlugin"
    }
};
FireBreath.pollInterval = 500;