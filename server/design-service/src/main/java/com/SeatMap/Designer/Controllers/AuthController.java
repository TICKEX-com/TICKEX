package com.SeatMap.Designer.Controllers;

import com.SeatMap.Designer.Configurations.UserToUserDetails;
import com.SeatMap.Designer.Configurations.UserToUserDetailsService;
import com.SeatMap.Designer.Services.JWTService;
import com.SeatMap.Designer.Services.UserService;
import com.SeatMap.Designer.dtos.LoginResponse;
import com.SeatMap.Designer.dtos.LoginRequest;
import com.SeatMap.Designer.dtos.UserRequest;
import jakarta.servlet.http.HttpServletResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.Map;

@CrossOrigin(origins = {"http://localhost:3000"})
@RestController
@RequestMapping("/api/auth")
public class AuthController {
    @Autowired
    private UserToUserDetailsService userDetailsService;
    @Autowired
    private JWTService jwtService;
    @Autowired
    private AuthenticationManager authenticationManager;
    @Autowired
    private UserService userService;
    private static final int cookieExpiry = 18000;
    @PostMapping("/authenticate")
    public LoginResponse createAuthenticationToken(@RequestBody LoginRequest loginRequest, HttpServletResponse response) throws UsernameNotFoundException {
        authenticate(loginRequest.getEmail(), loginRequest.getPassword());
        final UserDetails userDetails = userDetailsService.loadUserByUsername(loginRequest.getEmail());
        final String token = jwtService.generateToken((UserToUserDetails) userDetails);

        return LoginResponse.builder()
                .token(token).username(userDetails.getUsername()).build();
    }
    private void authenticate(String email, String password) throws UsernameNotFoundException {
        Authentication auth = authenticationManager.authenticate(new UsernamePasswordAuthenticationToken(email, password));
        if (!auth.isAuthenticated()) {
            throw new UsernameNotFoundException("invalid User Request");
        }
    }
    @PostMapping("/signup")
    public ResponseEntity<Map<String, String>> addNewUser(@RequestBody UserRequest userRequest) {
        userService.addUser(userRequest);
        Map<String, String> response = new HashMap<>();
        response.put("username", userRequest.getUsername());
        return ResponseEntity.status(HttpStatus.CREATED).body(response);
    }

}
