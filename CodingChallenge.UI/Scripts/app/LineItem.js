define(function (require) {
    var ko = require("knockout");
    var utilities = require("utilities");

    function LineItem() {
        var self = this;

        self.description = ko.observable();
        self.perPayPeriod = ko.observableArray();
        self.perAnnum = ko.observable();
        self.isDiscount = ko.computed(function () {
            return self.perAnnum() < 0;
        });

        self.perAnnumDisplay = ko.computed(function () {
            var result = utilities.formatCurrency(self.perAnnum());

            return result;
        });
        self.perPayPeriodDisplay = ko.computed(function() {
            var result = self.perPayPeriod().map(function(value, index) {
                return {
                    description: (index + 1),
                    value: utilities.formatCurrency(value)
                };
            });

            return result;
        });
        self.isExpanded = ko.observable(false);
        self.isCollapsed = ko.computed(function() {
            return !self.isExpanded();
        });
        self.toggleExpand = function() {
            self.isExpanded(!self.isExpanded());
        };
        
        self.fromApiModel = function(source) {
            source = source || {};

            self.description(source.description);
            self.perPayPeriod(source.perPayPeriod);
            self.perAnnum(source.perAnnum);
        };

        return self;
    };

    return LineItem;
});