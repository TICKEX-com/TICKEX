package com.SeatMap.Designer.dtos;

import com.SeatMap.Designer.Models.Design;
import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.springframework.data.mongodb.core.mapping.DBRef;

import java.util.List;
@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class UserRequest {
    @NotBlank(message="Username Shouldn't Be Null Or Empty ! ")
    @NotNull(message="Name Shouldn't Be Null Or Empty ! ")
    private String username;
    @Email
    private String email;
    @NotBlank(message="Password Shouldn't Be Null Or Empty ! ")
    @NotNull(message="Password Shouldn't Be Null Or Empty ! ")
    private String password;
}
