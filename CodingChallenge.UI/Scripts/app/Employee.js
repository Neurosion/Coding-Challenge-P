define(function (require) {
    var ko = require("knockout");

    function Employee() {
        var self = this;

        self.firstName = ko.observable();
        self.lastName = ko.observable();
        return self;
    };

    return Employee;
});