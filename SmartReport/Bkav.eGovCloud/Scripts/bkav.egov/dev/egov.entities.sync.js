(function (egov) {

    // Quản lý các đối tượng đồng bộ dữ liệu với server
    // {
    //    name: "userConfig",  => Tên đối tượng để xác định đối tượng sẽ merge với client.
    //    request: egov.request.getCommonConfigs, => trong egov.request-manager.js
    //    option: {                               => jquery ajax option mặc định.
    //        beforeSent: function () {
    //            egov.pubsub.public(egov.events.status, {
    //                type: "processing",
    //                message: "Đang tải"
    //            });
    //        }
    //    }
    // }

    egov.entities.sync = {

        documentStore: {
            name: "documentStore",
            request: egov.request.syncDocumentStore,
            option: {
                beforeSent: function () {
                    egov.pubsub.public(egov.events.status, {
                        type: "processing",
                        message: "Đang tải"
                    });
                }
            }
        }
    };

})
(this.egov = this.egov || {})