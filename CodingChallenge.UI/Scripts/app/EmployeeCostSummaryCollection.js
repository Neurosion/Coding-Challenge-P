define(function (require) {
    var ko = require("knockout");
    var utilities = require("utilities");

    var EmployeeCostSummary = require("EmployeeCostSummary");

    function EmployeeCostSummaryCollection() {
        var self = this;

        self.items = ko.observableArray();
        self.totalPerAnnum = ko.computed(function () {
            var result= self.items().reduce(function (sum, item) {
                return sum + item.totalPerAnnum();
            }, 0);
            
            return result;
        });
        self.totalPerAnnumDisplay = ko.computed(function() {
            var result = utilities.formatCurrency(self.totalPerAnnum());

            return result;
        });
        self.hasItems = ko.computed(function() {
            return self.items().some(function(item) {
                return item != null && item.hasData();
            });
        });

        self.fromApiModel = function (source) {
            source = source || [];

            var items = source.map(function(costSummary) {
                var result = new EmployeeCostSummary();
                result.fromApiModel(costSummary);

                return result;
            });
            self.items(items);
        };

        return self;
    };

    return EmployeeCostSummaryCollection;
});