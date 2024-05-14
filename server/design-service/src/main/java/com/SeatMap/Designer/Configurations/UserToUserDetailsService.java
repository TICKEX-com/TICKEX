package com.SeatMap.Designer.Configurations;

import com.SeatMap.Designer.Models.User;
import com.SeatMap.Designer.Repositories.UserRepository;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Component
@Service
public class UserToUserDetailsService implements UserDetailsService {
    private final   UserRepository repos;

    public UserToUserDetailsService(UserRepository repos) {
        this.repos = repos;
    }

    @Override
    public UserDetails loadUserByUsername(String email) throws UsernameNotFoundException {
        Optional<User> u = repos.findByEmail(email);
        return u.map(UserToUserDetails::new).orElseThrow(()->new RuntimeException("user not found"));
    }
}
