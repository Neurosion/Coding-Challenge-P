define(function(require) {
    var ko = require("knockout");

    var Employee = require("Employee");

    function CostPreviewer() {
        var self = this;

        self.employees = ko.observableArray();

        self.addEmployee = function() {
            self.employees.push(new Employee());
        };

        return self;
    };

    return CostPreviewer;
});