package com.SeatMap.Designer.Services;

import com.SeatMap.Designer.Configurations.UserToUserDetails;
import com.SeatMap.Designer.Models.Design;
import com.SeatMap.Designer.Models.User;
import com.SeatMap.Designer.Repositories.DesignRepository;
import com.SeatMap.Designer.Repositories.UserRepository;
import com.SeatMap.Designer.dtos.DesignRequest;
import com.SeatMap.Designer.dtos.UserRequest;
import io.jsonwebtoken.ExpiredJwtException;
import org.springframework.dao.DuplicateKeyException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.awt.print.Pageable;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
public class UserService {
    private final UserRepository userRepository;
    private final PasswordEncoder passwordEncoder;
    private final DesignService designService;
    @Autowired
    public UserService(UserRepository userRepository, PasswordEncoder passwordEncoder, DesignService designService) {
        this.userRepository = userRepository;
        this.passwordEncoder = passwordEncoder;
        this.designService = designService;
    }

    public void addUser(UserRequest userRequest) {
        User user = User.builder()
                .email(userRequest.getEmail())
                .date_created(LocalDateTime.now())
                .username(userRequest.getUsername())
                .password(passwordEncoder.encode(userRequest.getPassword())).build();
        userRepository.save(user);
    }
    public void addDesign(String newDesignName ) {
        User user = getUser(getCurrentUser().getEmail());
        List<Design> designs = user.getMyDesigns();
        Design newDesign = designService.findDesignByName(newDesignName).orElseThrow();
        designs.add(newDesign);
        user.setMyDesigns(designs);
        userRepository.save(user);
    }

    public User getUser(String email) {
        return userRepository.findByEmail(email).orElseThrow(() -> new RuntimeException("user not found"));
    }
    public UserToUserDetails getCurrentUser(){
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        return (UserToUserDetails) authentication.getPrincipal();
    }

    public List<Design> getUserDesigns(String email) {
        User user = getUser(email);
        List<Design> userDesigns = user.getMyDesigns().stream()
                .map((id) -> designService.findDesignById(id.getId()))
                .filter(Optional::isPresent)
                .map(Optional::get).toList();
        return userDesigns;
    }
}