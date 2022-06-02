///Must add Jquery where it will be called.

function HasValue(value, isBoolType) {

    if (!value && !isBoolType) {
        return false
    }
    else if (isBoolType) {

        var strValue = "" + value

        if (!strValue) {
            return false
        }
        else {
            return true;
        }
    }
    else {
        return true;
    }
}

function IsNumeric(value) {

    if (!parseFloat("" + value) && parseFloat("" + value) != 0) {
        return false;
    }
    else {
        return true;
    }
}

function IsInt(value) {

    if (!parseInt("" + value) && parseInt("" + value) != 0) {
        return false;
    }
    else {
        return true;
    }
}

function IsValidComparison(value, min, max) {

    var errorCount = 0;

    if (IsNumeric(min)) {
        if (min > parseFloat(value)) {
            errorCount++;
        }
    }

    if (IsNumeric(max)) {

        if (max < parseFloat(value)) {
            errorCount++;
        }
    }

    if (errorCount) {
        return false
    }

    return true;

}


function isValid(value, isBoolType, hasValue, isNumeric, isInt, min, max) {

    if (hasValue) {
       return HasValue(value, isBoolType);
    }

    if (isNumeric) {
        return IsNumeric(value);
    }

    if (isInt) {
        return IsInt(value);
    }

    if (min || max) {
        return IsInt(value);
    }
}

function changeColor(obj, color) {

    $(obj).css("background-color", color);

}

function isValidWithError(obj, fieldName, isInput, isBoolType, hasValue, isNumeric, isInt, min, max, isAlert, errorColor, defaultColor) {

    var value = isInput ? $(obj).val() : $(obj).text();

    if (hasValue) {

        if (!HasValue(value, isBoolType)) {
            if (isAlert) {
                alert(fieldName+ " is mandatory.");
            }
            if (errorColor) {
                changeColor(obj, errorColor);
            }
            return false;
        }
        else if (defaultColor) {
            changeColor(obj, defaultColor);
        }
    }

    if (isNumeric) {

        if (!IsNumeric(value)) {

            if (isAlert) {
                alert(fieldName + " must be numeric.");
            }

            if (errorColor) {
                changeColor(obj, errorColor);
            }

            return false;
        }
        else if (defaultColor) {
            changeColor(obj, defaultColor);
        }

    }

    if (isInt) {

        if (!IsInt(value)) {

            if (isAlert) {
                alert("Enter a valid value without decimal point for "+fieldName);
            }
            if (errorColor) {
                changeColor(obj, errorColor);
            }

            return false;
        }
        else if (defaultColor) {
            changeColor(obj, defaultColor);
        }
    }

    if (IsNumeric(min) || IsNumeric(max)) {

        if (!IsValidComparison(value, min, max)) {

            if (isAlert) {
                alert("Enter a valid value for " + fieldName);
            }
            if (errorColor) {
                changeColor(obj, errorColor);
            }

            return false;
        }
        else if (defaultColor) {
            changeColor(obj, defaultColor);
        }
    }

    return true;
}
