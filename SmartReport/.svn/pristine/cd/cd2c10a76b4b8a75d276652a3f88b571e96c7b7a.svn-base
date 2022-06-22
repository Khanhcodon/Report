(function () {
    var AppView = Backbone.View.extend({
        el: ".content-wrapper",
        events: {
            "click #ChooseIndicator": "renderModalIndicators"
        },
        initialize: function (options) {
            //$.jstree.defaults.core.dblclick_toggle = true;
            var that = this;
            that.render();
        },
        render: function () {
            var that = this;
            that.renderTargets();
        },
        renderTargets: function () {
            $.ajax({
                url:"/Admin/Targets/GetIndicatorsss",
                type: 'Get',
                error: function (a, b, c) {
                },
                success: function (response) {
                    //debugger
                    var departments = _.map(response, function (dp) {
                        return {
                            id: dp.id,
                            text: dp.text,
                            parent: dp.parent == 0 ? "#" : dp.parent,
                            type: "tree",
                            icon: "fa fa-home"
                        }
                    });
                    departments[0].state = {
                        selected: true
                    }
                    $("#department").jstree({
                            "plugins": ["search"],
                            'core': {
                                'data': departments,
                            },
                            "types": {
                                "tree": { "icon": "mdi mdi-email" }
                            },
                        }).bind("loaded.jstree", function (event, data) {
                            // you get two params - event & data - check the core docs for a detailed description
                            $(this).jstree("open_all");
                        });
                    var to = false;
                    $('#searchDepartment').keyup(function () {
                        $('#department').jstree(true).close_all();
                        if (to) { clearTimeout(to); }
                        to = setTimeout(function () {
                            var v = $('#searchDepartment').val();
                            if (!v) {
                                $('#department').jstree(true).open_all();
                            }
                            $('#department').jstree(true).search(v);
                        }, 250);
                    });
                    $("#department").on('changed.jstree', function (e, data) {
                        var i, j, r = [];
                        for (i = 0, j = data.selected.length; i < j; i++) {
                            r.push(data.instance.get_node(data.selected[i]).text);
                    }
                        $('#event_result').html(r.join(', '));
                    });
                }      
            });
        },
    });
    var appview = new AppView();
})();