package com.SeatMap.Designer.Controllers;


import com.SeatMap.Designer.Configurations.UserToUserDetails;
import com.SeatMap.Designer.Models.Design;
import com.SeatMap.Designer.Models.User;
import com.SeatMap.Designer.Services.UserService;
import com.SeatMap.Designer.dtos.UserResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Objects;
import java.util.Optional;

@CrossOrigin(origins = {"http://localhost:3000"},allowCredentials = "true")
@RestController
@RequestMapping("/api/user")
public class UserController {
    private final UserService userService;
    @Autowired
    public UserController(UserService userService ) {
        this.userService = userService;
    }
    @GetMapping("/me")
    public ResponseEntity<UserResponse> authenticatedUser() {
        UserToUserDetails user = userService.getCurrentUser();
        User currentUser = userService.getUser(user.getEmail());
        return ResponseEntity.ok(mapToUserResponse(currentUser));
    }

    private UserResponse mapToUserResponse(User currentUser) {
        return UserResponse.builder()
                .username(currentUser.getUsername())
                .email(currentUser.getEmail())
                .date_created(currentUser.getDate_created())
                .is_active(currentUser.getIs_active())
                .myDesigns(currentUser.getMyDesigns())
                .build();
    }
    @GetMapping("/my-designs")
    public List<Design> getUserDesings(){
        UserToUserDetails user = userService.getCurrentUser();
        return userService.getUserDesigns(user.getEmail());
    }
    @GetMapping("/designs/{name}")
    public ResponseEntity<Design> getUserDesing(@PathVariable String name ){
        UserToUserDetails user = userService.getCurrentUser();
        List<Design> myDesigns = getUserDesings();
        return myDesigns.stream()
                .filter((design)-> design.getName().equals(name))
                .findFirst()
                .map(ResponseEntity::ok)  // Wrap the found design in a ResponseEntity if present
                .orElseGet(() -> ResponseEntity.notFound().build());  // Return 404 Not Found if no design is found
    }

}
