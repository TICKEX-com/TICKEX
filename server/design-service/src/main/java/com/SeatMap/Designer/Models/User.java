package com.SeatMap.Designer.Models;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.hibernate.validator.constraints.UniqueElements;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.index.Indexed;
import org.springframework.data.mongodb.core.mapping.DBRef;
import org.springframework.data.mongodb.core.mapping.Document;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Builder
@Data
@AllArgsConstructor
@NoArgsConstructor
@Document("Users")
public class User {
    @Id
    private String id ;
    @Indexed(unique = true)
    private String username;
    @Indexed(unique = true)
    private String email;
    private String password;
    @Builder.Default
    private Boolean is_active=true;
    private LocalDateTime date_created;
    @DBRef
    @Builder.Default
    private List<Design> myDesigns= new ArrayList<>();
}
