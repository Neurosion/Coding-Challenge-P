(function() {
    requirejs.config({
        baseUrl: "../Scripts/app",
        paths: {
            "jquery": "../jquery-1.10.2.min",
            "knockout": "../knockout-2.3.0"
        }
    });

    requirejs(["jquery", "knockout", "CostPreviewer"],
        function ($, ko, CostPreviewer) {
            var container = $("#preview-container")[0];
            var model = new CostPreviewer();
            model.addEmployee();

            ko.applyBindings(model, container);
        });
})();