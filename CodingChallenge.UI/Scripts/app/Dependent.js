define(function (require) {
    var ko = require("knockout");

    function Dependent() {
        var self = this;

        self.firstName = ko.observable();
        self.lastName = ko.observable();

        self.hasData = ko.computed(function() {
            return self.firstName() != null
                || self.lastName() != null;
        });

        self.toApiModel = function () {
            if (!self.hasData())
                return null;

            return {
                FirstName: self.firstName(),
                LastName: self.lastName()
            };
        };

        self.fromApiModel = function(source) {
            source = source || {};

            self.firstName(source.firstName);
            self.lastName(source.lastName);
        };

        return self;
    };

    return Dependent;
});