package com.SeatMap.Designer.Handlers;

import io.jsonwebtoken.ExpiredJwtException;
import org.springframework.core.Ordered;
import org.springframework.core.annotation.Order;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestControllerAdvice;
import org.springframework.web.servlet.mvc.method.annotation.ResponseEntityExceptionHandler;
import org.springframework.dao.DuplicateKeyException;
import java.util.HashMap;
import java.util.Map;

@Order(Ordered.HIGHEST_PRECEDENCE)
@RestControllerAdvice
public class RestExceptionHandler {
    @ExceptionHandler(MethodArgumentNotValidException.class)
    public Map<String,String> handleMethodArgumentNotValidException(MethodArgumentNotValidException ex) {
        Map<String, String> errorMap = new HashMap<>();
        ex.getBindingResult().getFieldErrors().forEach((field) -> errorMap.put(field.getField(), field.getDefaultMessage()));
        return errorMap;
    }
    @ExceptionHandler(DuplicateKeyException.class)
    public Map<String,String> handleDuplicateKeyExceptionException(DuplicateKeyException ex) {
        Map<String, String> errorMap = new HashMap<>();
        errorMap.put("message","User Data Already Used");
        return errorMap;
    }
    @ExceptionHandler(ExpiredJwtException.class)
    public Map<String,String> handleExpiredJwtException(ExpiredJwtException ex){
        Map<String, String> errorMap = new HashMap<>();
        errorMap.put("message","Token Expired , Please Log In");
        return errorMap;
    }

}
