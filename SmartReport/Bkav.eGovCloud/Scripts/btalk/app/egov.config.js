define(function(){
    
    var _ALERT_TARGET = "#message_panel";

    window._CLIENT_TYPE = "egov";
    window._EGOVURL = "http://eoffice.bkavegov.vn";
    window._FILEURL = "https://efile.tayninh.gov.vn";
    
    /** Configuration for app */
	egov.config = {
	    SITE_NAME: window._DOMAIN_OF_ACCOUNT,
		// Thong tin dung chung
		CLIENT: {
			CLIENT_NAME: "eGov messenger",
			DESCRIPTION: "eGov messenger - Hội thoại trực tuyến",
			MYSTYLE: 'default',
			CLIENT_VERSION: 'EGOV-1.0.0.0',
			STYLE_SHEET: "jwchat.css",
			THEMESDIR: "themes/default",
			// egov: Khi trien khai kem eGov
		    // btalk.vn: khi trien khai nhu btalk client
            // bkavpro: khi triển khai chat hỗ trợ khách hàng qua bkav pro
			CLIENT_TYPE: window._CLIENT_TYPE,
			ACS_ACTIVE: false
		},
		VIEW: {
			// Dung khi trien khai noi bo cong ty
		    // AVATAR_URL: "https://danhba.bkav.com/avatars/{0}.bmp"
			// Dung khi trien khai eGov cho khach hang
		     AVATAR_URL: window._EGOVURL + "/AvatarProfile/{0}.jpg"
		},

		// Thong tin ket noi cua XMPP server
		XMPP: {
			PORT: 8888,
			HOST: "https://chat.bkav.com/bosh/",
			SECURE: true,

			HTTPBASE: "/bosh/",
			TYPE: "binding",
			SERVERS_ALLOWED: [egov.setting.user.defaultDomain], //this.SITENAME
			DEFAULT_RESOURCE: 'egov',
			DEFAULT_PRIORITY: "10",

			// most probably you don't want to change anything below
			TIMERVAL: 2000,                             // poll frequency in msec
			DEFAULT_CONFERENCE_ROOM: "talks",
			DEFAULT_CONFERENCE_SERVER: "conference." + egov.setting.user.defaultDomain //this.SITENAME,
		},
		// Co che debug dung cho cho client
		DEBUG: {
			// debug option
			ACTIVE: false,                              // turn debugging on
			DEBUG_LVL: 2, // debug-level 0..4 (4: very noisy)
			USE_DEBUGJID: false, // if true only DEBUGJID gets the debugger
			DEBUGJID: "cuongnt@" + egov.setting.user.defaultDomain, //this.SITENAME, // which user get's debug messages
			// Custome debugger: Debugger.js
			DEBUGGER: window.Debugger
		},
		// btalk.auth.js su dung
		AUTH: {
			xmppTokenKey: "bkavAuthen",
			keystoneTokenKey: "keystoneAuth",
			loginPage: "index.html",
			authorizedPage: "jwchat.html",
			rXmppToken: { path:'/' },                   // {domain: '.bkav.com', path:'/'},
			rKeystoneToken: { path:'/'},                // {domain: '.bkav.com', path:'/'},
			wXmppToken: { expires: 7, path: '/' },      // { expires: 7, domain: '.bkav.com', path: '/' },
			wKeystoneToken: { expires: 7, path: '/' }   // { expires: 7, domain: '.bkav.com', path: '/' },
		},
		// btalk.egov.js su dung
		WEBAPI: {
		    url: window._EGOVURL + "/webapi/egovapi/getdeptandusers",
		    userDeptUrl: window._EGOVURL + "/webapi/user/GetAllUserDepartmentJobTitlesPosition",
		    //allUsersUrl: window._EGOVURL + "/webapi/EgovApi/GetAllUsers",
		    allUsersUrl: window._EGOVURL + "/webapi/user/GetAllUsers",
		    allDeptUrl: window._EGOVURL + "/webapi/user/GetAllDepartment",
		    allJobTitlesUrl: window._EGOVURL + "/webapi/user/GetAllJobTitles",
		    allPositionsUrl: window._EGOVURL + "/webapi/user/GetAllPositions",
			acsUrl: "https://bkavacs.bkav.com/ACS_Webservice.asmx/GetListAccountVacationAndGoTaskForChat"
		},

		FILESERVER: {
		    keystoneUrl: window._FILEURL + ":5000/v2.0/",
		    keystoneUrlAdmin: window._FILEURL + ":35357/v2.0/",
			tenant: "",
			debug: false,
			defaultRegion: "regionOne",
			ACTIVE: window._FILESERVER_ACTIVE,
			MAX_SIZE: 100 //Tinh theo MB
		},

		ACS: {
		    acsUrl: "https://bkavacs.bkav.com/ACS_Webservice.asmx/GetListAccountVacationAndGoTaskForChat"
		},

		ALERT: {
		    target: _ALERT_TARGET
		    //, callbackAfterShow: function(){}
		},

		SUPPORT: {
		    DEFAULT_SUPPORT_GROUP_EXTRA: "_support_bkavpro",
		}
	};

	egov.config = $.extend(true, {}, egov.config, {
		AUTH: {
			domain: egov.config.XMPP.SERVERS_ALLOWED[0],
			rXmppToken: { domain: '.' + egov.config.SITE_NAME, path:'/' },
			rKeystoneToken: { domain: '.' + egov.config.SITE_NAME, path:'/'},
			wXmppToken: { expires: 7, domain: '.' + egov.config.SITE_NAME, path: '/' },
			wKeystoneToken: { expires: 7, domain: '.' + egov.config.SITE_NAME, path: '/' },
            FILE_ACTIVE: egov.config.FILESERVER.ACTIVE || false
		},
		/** XMPP Connection Manager: Configuration cho btalk.connectionManager.js */
		CM: {
			debug: {
				Debugger: egov.config.DEBUG.DEBUGGER,
				// Turn debugging on
				active: egov.config.DEBUG.ACTIVE,
				// If true only debugJid gets the debugger
				useDebugJid: egov.config.DEBUG.USE_DEBUGJID,
				// Which user get's debug messages
				debugJid: egov.config.DEBUG.DEBUGJID,
				// Debug-level 0..4 (4 = very noisy)
				debugLvl: egov.config.DEBUG.DEBUG_LVL
			},

			xmpp: {
				connect_port: egov.config.XMPP.PORT,
				connect_host: egov.config.XMPP.HOST,
				connect_secure: egov.config.XMPP.SECURE,

				domain: egov.config.XMPP.SERVERS_ALLOWED[0],				// domain cua tai khoan dang nhap
				conference: egov.config.XMPP.DEFAULT_CONFERENCE_SERVER,
				backendType: egov.config.XMPP.TYPE,
				defaultResource: egov.config.XMPP.DEFAULT_RESOURCE,
				httpbase: egov.config.XMPP.HTTPBASE,
				baseDatetimeQuery: '2015-01-01T00:00:00Z',
				chatterNextCounts: 15,
				messageNextCounts: 40,
				disco: null
			},

			cookie: {
				expiredDay: 7,
				// br: btalk resource
				resourceKey: "br"
			},

			client: {
				version: egov.config.CLIENT.CLIENT_VERSION
			},

			file: {
				seperatekey: "!@!"
			},

			loginPage: "index.html",

			support: {
			    conferenceExtra: egov.config.SUPPORT.DEFAULT_SUPPORT_GROUP_EXTRA,
			}
		},

		/** File Manager: Configuration cho btalk.fileManager.js */
		FM: {
		}
	});
});