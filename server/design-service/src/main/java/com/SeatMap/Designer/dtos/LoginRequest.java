package com.SeatMap.Designer.dtos;

import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class LoginRequest {
    @Email
    private String email;
    @NotBlank(message="Password Shouldn't Be Null Or Empty ! ")
    @NotNull(message="Password Shouldn't Be Null Or Empty ! ")
    private String password;
}
