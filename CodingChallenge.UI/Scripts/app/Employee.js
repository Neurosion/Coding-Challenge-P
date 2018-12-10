define(function (require) {
    var ko = require("knockout");
    var Dependent = require("Dependent");

    function Employee() {
        var self = this;

        self.firstName = ko.observable();
        self.lastName = ko.observable();
        self.dependents = ko.observableArray();
        self.hasDependents = ko.computed(function() {
            return self.dependents().length > 0;
        });
        self.doesNotHaveDependents = ko.computed(function () {
            return !self.hasDependents();
        });
        self.addDependent = function () {
            self.dependents.push(new Dependent());
        };
        return self;
    };

    return Employee;
});