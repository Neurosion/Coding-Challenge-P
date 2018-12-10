define(function() {
    return {
        isEmptyString: function (value) {
            var result = value == null
                || /^\s*$/g.test(value);

            return result;
        },
        formatCurrency: function(value) {
            var result = parseFloat(Math.abs(value)).toFixed(2).toString();
            result = result.replace(/\B(?=(\d{3})+(?!\d))/g, ",");

            if (value < 0)
                result = "(" + result + ")";

            return result;
        }
    };
});