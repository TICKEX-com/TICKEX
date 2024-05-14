package com.SeatMap.Designer.dtos;

import com.SeatMap.Designer.Enums.LocationType;
import com.SeatMap.Designer.Enums.StateEnum;
import jakarta.validation.constraints.NotBlank;

import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class DesignRequest {
    @NotBlank(message="Name Shouldn't Be Null Or Empty ! ")
    @NotNull(message="Name Shouldn't Be Null Or Empty ! ")
    private String name;
    @NotBlank(message="Description Shouldn't Be Null Or Empty ! ")
    private String description;
    @NotNull(message="Location  Shouldn't Be Null Or Empty ! ")
    @NotBlank(message="Location Shouldn't Be Null Or Empty ! ")
    private String locationType;
    @Size(min=1,message="Categories Shouldn't  Or Empty !")
    private List<CategoryRequest> categories;
}
