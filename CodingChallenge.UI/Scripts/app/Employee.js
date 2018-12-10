define(function (require) {
    var ko = require("knockout");
    var utilities = require("utilities");
    var Dependent = require("Dependent");

    function Employee() {
        var self = this;

        self.firstName = ko.observable();
        self.lastName = ko.observable();
        self.fullName = ko.computed(function() {
            return [self.firstName(), self.lastName()].join(" ");
        });
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

        self.fromApiModel = function(source) {
            source = source || {};

            self.firstName(source.firstName);
            self.lastName(source.lastName);

            source.dependents = source.dependents || [];

            var mappedDependents = source.dependents.map(function(dependent) {
                var result = new Dependent();
                result.fromApiModel(dependent);

                return result;
            });

            self.dependents(mappedDependents);
        };

        return self;
    };

    return Employee;
});