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

        self.hasData = ko.computed(function () {
            var result = [self.firstName(), self.lastName()].some(function (item) {
                return !utilities.isEmptyString(item);
            });

            return result;
        });

        self.toApiModel = function () {
            if (!self.hasData())
                return null;

            return {
                FirstName: self.firstName(),
                LastName: self.lastName(),
                Dependents: self.dependents().map(function (dependent) {
                    return dependent.toApiModel();
                })
            };
        };

        return self;
    };

    return Employee;
});