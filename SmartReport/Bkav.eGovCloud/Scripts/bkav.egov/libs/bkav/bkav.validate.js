(function (egov) {
    var multiEmailRegex = /^(?:[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+\.)*[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\.)){0,61}[a-zA-Z0-9]?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\[(?:(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\]))([;,](?:[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+\.)*[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\.)){0,61}[a-zA-Z0-9]?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\[(?:(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\])))*$/;

    var emailRegex = /^(?:[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+\.)*[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\.)){0,61}[a-zA-Z0-9]?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\[(?:(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\]))$/;

    var dateTimeRegex = /^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$/;

    var ipRegex = /^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$/;

    var domainRegex = /^[a-zA-Z0-9]+([a-zA-Z0-9\-\.]+)?\.(vn|com|org|net|mil|edu|VN|COM|ORG|NET|MIL|EDU)$/;

    var multiPhoneRegex = /^(\+?[0-9. ()-]{10,12})([;,](\+?[0-9. ()-]{10,12}))*$/;

    var phoneRegex = /^\+?[0-9. ()-]{10,12}$/;

    var cmndRegex = /^(\d{9}|\d{12})$/;

    var documentCodeRegex = /.*\$N\$.*/;
    
    var roleKeyRegex = /^(?=.*[A-Za-z0-9])[\S]*$/;

    egov.validate = {};

    egov.validate.multiEmail = function (input) {
        return multiEmailRegex.test(input);
    };

    egov.validate.email = function (input) {
        return emailRegex.test(input);
    };

    egov.validate.dateTime = function (input) {
        return dateTimeRegex.test(input);
    };

    egov.validate.ip = function (input) {
        return ipRegex.test(input);
    };

    egov.validate.domain = function (input) {
        return domainRegex.test(input);
    };

    egov.validate.multiPhone = function (input) {
        return multiPhoneRegex.test(input);
    };

    egov.validate.phone = function (input) {
        return phoneRegex.test(input);
    };

    egov.validate.cmnd = function (input) {
        return cmndRegex.test(input);
    };

    egov.validate.documentCode = function (input) {
        return documentCodeRegex.test(input);
    };

    egov.validate.roleKey = function (input) {
        return roleKeyRegex.test(input);
    };
})(window.egov = window.egov || {})
