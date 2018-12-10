define(function (require) {
    var ko = require("knockout");
    var utilities = require("utilities");

    var Employee = require("Employee");
    var LineItem = require("LineItem");

    function EmployeeCostSummary() {
        var self = this;

        self.employee = ko.observable();
        self.lineItems = ko.observableArray();
        self.totalPerAnnum = ko.computed(function () {
            var result = self.lineItems().reduce(function (sum, lineItem) {
                return sum + lineItem.perAnnum();
            }, 0);
            
            return result;
        });
        self.totalPerAnnumDisplay = ko.computed(function () {
            var result = utilities.formatCurrency(self.totalPerAnnum());

            return result;
        });
        self.hasData = ko.computed(function() {
            var result = self.employee() != null
                && self.employee().hasData();

            return result;
        });

        self.fromApiModel = function(source) {
            source = source || {};

            var employee = new Employee();
            employee.fromApiModel(source.employee);
            self.employee(employee);

            var lineItems = (source.lineItems || []).map(function(lineItem) {
                var result = new LineItem();
                result.fromApiModel(lineItem);

                return result;
            });

            self.lineItems(lineItems);
        };
        
        return self;
    };

    return EmployeeCostSummary;
});