$.validator.addMethod('numberonly', function (value, element, params) {
    var regex = new RegExp('^\\d*$');
    return regex.test(value);
});

$.validator.unobtrusive.adapters.add('numberonly', [], function (options) {
    options.rules['numberonly'] = true;
    options.messages['numberonly'] = options.message;
});
