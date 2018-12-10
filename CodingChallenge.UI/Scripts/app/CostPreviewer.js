define(function(require) {
    var ko = require("knockout");
    var $ = require("jquery");

    var Employee = require("Employee");
    var EmployeeCostSummaryCollection = require("EmployeeCostSummaryCollection");

    function CostPreviewer() {
        var self = this;

        self.employees = ko.observableArray();
        self.costSummaries = ko.observable(new EmployeeCostSummaryCollection());
        
        self.updateCostSummaries = function() {
            var requestData = {
                Employees: self.employees().map(function(employee) {
                    return employee.toApiModel();
                })
            };

            $.ajax({
                url: "api/Calculate/CostSummary",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(requestData),
                dataType: "json"
            }).success(function (responseData, status, xhr) {
                var costSummaries = new EmployeeCostSummaryCollection();
                costSummaries.fromApiModel(responseData);

                self.costSummaries(costSummaries);
            }).error(function (xhr, status, message) {
                console.error(status + ": " + message + "\n" + xhr.responseText);
            });
        };

        self.addEmployee = function() {
            self.employees.push(new Employee());
        };

        return self;
    };

    return CostPreviewer;
});