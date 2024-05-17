package com.SeatMap.Designer.Services;

import com.SeatMap.Designer.Configurations.UserToUserDetails;
import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.SignatureAlgorithm;

import org.springframework.stereotype.Component;

import java.util.Date;
import java.util.HashMap;
import java.util.Map;
import java.util.function.Function;

import io.jsonwebtoken.Claims;

@Component
public class JWTService {
    public static final long JWT_TOKEN_VALIDITY = 5 * 60 * 60 * 1000;

    private final String secret="THIS IS OUR PROJECT, HERE IS OUR SECRET";
    private final String issuer="auth-api";
    private final String audience="client";

    public String getUsernameFromToken(String token) {
        return getClaimFromToken(token, claims->claims.get("username",String.class) );
    }
    public String getEmailFromToken(String token) {
        return getClaimFromToken(token, claims->claims.get("email",String.class) );
    }
    //retrieve expiration date from jwt token
    public Date getExpirationDateFromToken(String token) {
        return getClaimFromToken(token, Claims::getExpiration);
    }
    public <T> T getClaimFromToken(String token, Function<Claims, T> claimsResolver) {
        final Claims claims = getAllClaimsFromToken(token);
        return claimsResolver.apply(claims);
    }
    //for retrieveing any information from token we will need the secret key
    private Claims getAllClaimsFromToken(String token) {
        return Jwts.parser()
                .setSigningKey(secret.getBytes())
                .requireIssuer(issuer)
                .requireAudience(audience)
                .parseClaimsJws(token).getBody();
    }
    //check if the token has expired
    private Boolean isTokenExpired(String token) {
        final Date expiration = getExpirationDateFromToken(token);
        return expiration.before(new Date());
    }
    public String generateToken(UserToUserDetails userDetails) {
        Map<String, Object> claims = new HashMap<>();
        return doGenerateToken(claims, userDetails.getUsername(), userDetails.getEmail());
    }
    private String doGenerateToken(Map<String, Object> claims, String userName, String email) {
        claims.put("email", email);
        claims.put("username", userName);
        return Jwts.builder()
                .setClaims(claims)
                .setAudience(audience)
                .setIssuer(issuer)
                .setIssuedAt(new Date(System.currentTimeMillis()))
                .setExpiration(new Date(System.currentTimeMillis() + JWT_TOKEN_VALIDITY))
                .signWith(SignatureAlgorithm.HS512, secret).compact();
    }
    public Boolean validateToken(String token, UserToUserDetails userDetails) {
        final String username = getUsernameFromToken(token);
        return (username.equals(userDetails.getUsername()) && !isTokenExpired(token));
    }

}
