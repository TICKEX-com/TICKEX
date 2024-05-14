package com.SeatMap.Designer.Models;

import com.SeatMap.Designer.Enums.LocationType;
import com.SeatMap.Designer.Enums.StateEnum;
import jdk.jshell.Snippet;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.springframework.data.annotation.CreatedDate;
import org.springframework.data.annotation.Id;
import org.springframework.data.annotation.LastModifiedDate;
import org.springframework.data.mongodb.core.index.Indexed;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.format.annotation.DateTimeFormat;

import java.time.LocalDateTime;
import java.util.List;

@Document("Design")
@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class Design {
    @Id
    private String id;

    @Indexed(unique = true)
    private String name;

    private String description;

    @Builder.Default
    private StateEnum state = StateEnum.DRAFT;

    private List<Category> categories;
    private String locationType;

    @DateTimeFormat(iso = DateTimeFormat.ISO.DATE_TIME)
    @CreatedDate
    private LocalDateTime created_at;

    @DateTimeFormat(iso = DateTimeFormat.ISO.DATE_TIME)
    @LastModifiedDate
    private LocalDateTime updated_at;
    @Builder.Default
    private Integer duplicate = 0;
    private String content;
}
