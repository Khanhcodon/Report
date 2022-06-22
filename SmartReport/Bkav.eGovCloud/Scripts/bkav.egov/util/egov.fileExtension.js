(function (egov) {
    var _readonlyExtensions = ['pdf', 'png', 'gif', 'jpg', 'jpeg', 'bmp'];
    var _signRegex = /(.doc|.docx|.pdf|.xls|.xlsx)$/i;

    var FileExtension = {
        getFileName: function (fileName) {
            var _dotIndex = fileName.lastIndexOf(".");
            if (_dotIndex === -1) return fileName;

            return fileName.substring(0, _dotIndex);
        },

        getExtension: function (fileName) {
            var _dotIndex = fileName.lastIndexOf(".");
            if (_dotIndex === -1) return "";

            var ext = fileName.substring(_dotIndex + 1, fileName.length);
            return ext.toLowerCase();
        },

        getExtensionWithDot: function (fileName) {
            var ext = this.getExtension(fileName);
            return ext === "" ? "" : ("." + ext);
        },

        isPdf: function (fileName) {
            var ext = this.getExtensionWithDot(fileName);
            return ext === ".pdf";
        },

        isMsOfficeFile: function (fileName) {
            var ext = this.getExtensionWithDot(fileName);
            return ext === ".doc" || ext === ".docx";
        },

        isReadonly: function (fileName) {
            var extension = this.getExtension(fileName);
            return _readonlyExtensions.indexOf(extension) >= 0;
        },

        isForSign: function (fileName) {
            var ext = this.getExtensionWithDot(fileName);
            return _signRegex.test(ext);
        },

        getSizeText: function (filesize) {
            var oneKiloByte = 1024;
            var oneMegaByte = 1048576;
            var oneGigaByte = 1073741824;

            if (filesize >= oneGigaByte) {
                return Math.round(filesize / oneGigaByte) + " GB";
            }
            if (filesize >= oneMegaByte) {
                return Math.round(filesize / oneMegaByte) + " MB";
            }
            if (filesize >= oneKiloByte) {
                return Math.round(filesize / oneKiloByte) + " KB";
            }

            return filesize + " bytes";
        },

        downloadUri: function (uri, name) {
            var link = document.createElement("a");
            link.download = name;
            link.href = uri;
            link.style.display = 'none';
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
            delete link;
        }
    };

    egov.fileExtension = FileExtension;
})(window.egov || {});