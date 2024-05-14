package com.SeatMap.Designer.Configurations;

import com.SeatMap.Designer.Models.Design;
import com.SeatMap.Designer.Models.User;
import lombok.Getter;
import org.springframework.data.mongodb.core.mapping.DBRef;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;

import java.util.Collection;
import java.util.List;
import java.util.function.Consumer;

public class UserToUserDetails implements UserDetails {
    private String username;

    @Getter
    private String email;
    private String password;

    public UserToUserDetails(User user) {
        username=user.getUsername();
        email= user.getEmail();
        password= user.getPassword();
    }

    @Override
    public Collection<? extends GrantedAuthority> getAuthorities() {
        return null;
    }

    @Override
    public String getPassword() {
        return password;
    }

    @Override
    public String getUsername() {
        return username;
    }

    @Override
    public boolean isAccountNonExpired() {
        return true;
    }

    @Override
    public boolean isAccountNonLocked() {
        return true;
    }

    @Override
    public boolean isCredentialsNonExpired() {
        return true;
    }

    @Override
    public boolean isEnabled() {
        return true;
    }
}
