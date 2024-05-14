package com.SeatMap.Designer.Validators;

import com.SeatMap.Designer.Enums.StateEnum;
import jakarta.validation.ConstraintValidator;
import jakarta.validation.ConstraintValidatorContext;

public class DesignStateValidator implements ConstraintValidator<ValidateDesignState, String> {

    @Override
    public boolean isValid(String s, ConstraintValidatorContext constraintValidatorContext) {
        if (s == null) {
            return false;
        }
        try {
            Enum.valueOf(StateEnum.class, s);
            return true;
        } catch (IllegalArgumentException e) {
            return false;
        }
    }

}
