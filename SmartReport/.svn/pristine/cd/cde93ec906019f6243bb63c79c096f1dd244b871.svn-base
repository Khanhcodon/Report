// Tiêu chí
var RateEmployee = Backbone.Model.extend({
    defaults: function () {
        return {
            RateEmployeeId: -1,
            DepartmentId: 1,
            ParentId: 0,
            Name: "",
            Description: "",
            Point: 20,
            IsActived: true,
            ParentRateEmployee:null,
            RateEmployeeChildrens: null,
            Department: null,
            CheckInfringes: null,
            DepartmentName: "",
            DepartmentIdExt: 1
        };
    }
});
var ListRateEmployee = Backbone.Collection.extend({
    model: RateEmployee,
    remaining: function (ID) {
        return this.where({ ParentId: ID });
    }
});

var InfringeEmployee = Backbone.Model.extend({
    defaults: function () {
        return {
            CheckInfringeId: -1,
            Date: "",
            Detail:"",
            InfringeUserId: 1,
            CreateUserId: -1,
            RateEmployeeId: 1,
            IsActived: true,
            Cause: ""
        };
    }
});
var ListInfringeEmployee = Backbone.Collection.extend({
    model: RateEmployee,
    remaining: function (ID) {
        return this.where({ ParentId: ID });
    }
});