define(['jquery', 'underscore', './btalk/btalk'], function ($, _, btalk) {
    /** Configuration for app */
    btalk.config = {
        SITE_NAME: "btalk.vn",
        CLIENT: {
            CLIENT_NAME: "Btalk messenger",
            DESCRIPTION: "Btalk messenger - Hội thoại trực tuyến",
            MYSTYLE: 'default',
            CLIENT_VERSION: 'EGOV-1.0.0.0',
            STYLE_SHEET: "jwchat.css",
            THEMESDIR: "themes/default"
        },
        XMPP: {
            PORT: 8888,
            HOST: "chat.bkav.com",
            SECURE: true,
            HTTPBASE: "/bosh/",
            TYPE: "binding",
            SERVERS_ALLOWED: ["btalk.vn"], //this.SITENAME
            DEFAULT_RESOURCE: 'btalk',
            DEFAULT_PRIORITY: "10",

            // most probably you don't want to change anything below
            TIMERVAL: 2000, // poll frequency in msec
            DEFAULT_CONFERENCE_ROOM: "talks",
            DEFAULT_CONFERENCE_SERVER: "conference." + "btalk.vn" // this.SITENAME,
        },
        DEBUG: {
            // debug option
            ACTIVE: false, // turn debugging on
            DEBUG_LVL: 2, // debug-level 0..4 (4: very noisy)
            USE_DEBUGJID: false, // if true only DEBUGJID gets the debugger
            DEBUGJID: "cuongnt@btalk.vn", // this.SITENAME, // which user get's debug messages
            // Custome debugger: Debugger.js
            DEBUGGER: window.Debugger
        },
        AUTH: {
            xmppTokenKey: "bkavAuthen",
            keystoneTokenKey: "keystoneAuth",
            loginPage: "index.html",
            authorizedPage: "jwchat.html",
            rXmppToken: { domain: '', path: '/' },// {domain: '.btalk.vn', path:'/'},
            rKeystoneToken: { domain: '', path: '/' },// {domain: '.btalk.vn', path:'/'},
            wXmppToken: { expires: 7, domain: '', path: '/' },// { expires: 7, domain: '.btalk.vn', path: '/' },
            wKeystoneToken: { expires: 7, domain: '', path: '/' }// { expires: 7, domain: '.btalk.vn', path: '/' },
        }
    };

    btalk.config = $.extend(true, {}, btalk.config, {
        AUTH: {
            domain: btalk.config.SITE_NAME,
            rXmppToken: { domain: '.' + btalk.config.SITE_NAME, path: '/' },
            rKeystoneToken: { domain: '.' + btalk.config.SITE_NAME, path: '/' },
            wXmppToken: { expires: 7, domain: '.' + btalk.config.SITE_NAME, path: '/' },
            wKeystoneToken: { expires: 7, domain: '.' + btalk.config.SITE_NAME, path: '/' }
        },
        /** XMPP Connection Manager: Configuration cho btalk.connectionManager.js */
        CM: {
            debug: {
                Debugger: btalk.config.DEBUG.DEBUGGER,
                // Turn debugging on
                active: btalk.config.DEBUG.ACTIVE,
                // If true only debugJid gets the debugger
                useDebugJid: btalk.config.DEBUG.USE_DEBUGJID,
                // Which user get's debug messages
                debugJid: btalk.config.DEBUG.DEBUGJID,
                // Debug-level 0..4 (4 = very noisy)
                debugLvl: btalk.config.DEBUG.DEBUG_LVL
            },

            xmpp: {
                connect_port: btalk.config.XMPP.PORT,
                connect_host: btalk.config.XMPP.HOST,
                connect_secure: btalk.config.XMPP.SECURE,
                domain: btalk.config.XMPP.SERVERS_ALLOWED[0],				// domain cua tai khoan dang nhap
                backendType: btalk.config.XMPP.TYPE,
                defaultResource: btalk.config.XMPP.DEFAULT_RESOURCE,
                httpbase: "/bosh/", // btalk.config.XMPP.HTTPBASE,
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
                version: btalk.config.CLIENT.CLIENT_VERSION
            },

            file: {
                seperatekey: "!@!"
            },

            loginPage: "index.html"
        },

        /** File Manager: Configuration cho btalk.fileManager.js */
        FM: {
        }
    });
});