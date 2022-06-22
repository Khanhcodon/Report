
var VoteModel = Backbone.Model.extend({
    defaults: function () {
        var end = new Date
        end.setHours(23);
        return {
            VoteId: 0,
            TimeBegin: new Date(),
            TimeEnd: end,
            TimeBeginFormat: "",
            VoteDetailId: 1,
            Title: "",
            IsMultiSelect: true,
            IsPublic: true,
            IsCommentDiff: true,
            IsViewResultImmediately: true,
            IsNotify: true,
            DepartmentsView: "",
            UsersView: "",
            DepartmentsVote: "",
            UsersVote: "",
            UsersVoted: "",
            UserIdCreate: 1,
            CurrentUserId: 0,
            UsernameCreate: '',
            CountUserVote:0,
            IsNow: false,
            IsVoted: false,
            IsCreate: false
        };
    },
});

var VoteDetailModel = Backbone.Model.extend({
    defaults: function () {
        return {
            VoteId: 0,
            VoteDetailId: 0,
            UserIdCreate: 0,
            UserIdsVote: "",
            TitleDetail: "",
            IsChecked: false,
            TotalVote: 0,
            Percent: 0,
            MaxVote: 0,
            IsMultiSelect: true,
            Option: {
                IsMultiSelect: true,
                IsPublic: true,
                IsCommentDiff: true,
                IsViewResultImmediately: true,
                IsNotify: true
            },
        };
    },
    toggle: function () {
        this.save({ IsChecked: !(this.get("IsChecked")) });
    },
    setNotCheck: function () {
        this.save({ IsChecked: false });
    }
});

var ListVote = Backbone.Collection.extend({
    model: VoteModel
});

var ListVoteDetail = Backbone.Collection.extend({
    model: VoteDetailModel,

    //setMaxVote: function () {
    //    debugger
    //    var max = this.max(function (player) {
    //        return player.get('TotalVote');
    //    });
    //    this.forEach(function (model, index) {
    //        model.set("MaxVote", max.get("TotalVote"));
    //    });
    //},
    
    comparator: function (item) {
        return item.get('TotalVote');
    },

    remaining: function () {
        return this.where({ IsChecked: false });
    },
});

var listVoteDetail = new ListVoteDetail();
