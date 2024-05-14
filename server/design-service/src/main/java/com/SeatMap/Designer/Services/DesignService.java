package com.SeatMap.Designer.Services;

import com.SeatMap.Designer.Configurations.UserToUserDetails;
import com.SeatMap.Designer.Enums.StateEnum;
import com.SeatMap.Designer.Models.Category;
import com.SeatMap.Designer.Models.Design;
import com.SeatMap.Designer.Models.User;
import com.SeatMap.Designer.Repositories.DesignRepository;
import com.SeatMap.Designer.dtos.CategoryRequest;
import com.SeatMap.Designer.dtos.DesignRequest;
import com.SeatMap.Designer.dtos.UserResponse;
import lombok.Getter;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Lazy;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;
import java.util.Objects;
import java.util.Optional;

@Service
public class DesignService {
    private final DesignRepository repo;
    @Getter
    private  UserService userService;
    @Autowired
    public DesignService( DesignRepository repo) {
        this.repo = repo;
    }
    @Autowired
    public void setUserService(@Lazy UserService userService) {
        this.userService = userService;
    }
    public void addDesign(DesignRequest designRequest) {
        Design new_design = Design.builder()
                .name(designRequest.getName())
                .description(designRequest.getDescription())
                .created_at(LocalDateTime.now())
                .state(StateEnum.DRAFT)
                .locationType(designRequest.getLocationType())
                .categories(designRequest.getCategories().stream().map(this::mapCatgory).toList()).build();
        repo.insert(new_design);
        userService.addDesign(designRequest.getName());
    }
    private Category mapCatgory(CategoryRequest categoryrequest) {
        return Category.builder()
                .label(categoryrequest.getLabel())
                .color(categoryrequest.getColor())
                .numberOfSeats(categoryrequest.getNumberOfSeats()).build();
    }
    public Optional<Design> findDesignById(String id) {
        return repo.findById(id);
    }
    public Optional<Design> findDesignByName(String name) {
        return repo.findByName(name);
    }
    public boolean updateContentById(String id, String newContent) {
        Design design = repo.findById(id).orElse(null);
        if (design != null) {
            design.setContent(newContent);
            repo.save(design);
            return true;
        }
        return false;
    }
    public boolean updateSpecsById(String id, DesignRequest newDesign) {
        Design design = repo.findById(id).orElse(null);
        if (design != null) {
            if (newDesign.getName() != null) {
                design.setName(newDesign.getName());
            }
            if (newDesign.getDescription() != null) {
                design.setDescription(newDesign.getDescription());
            }
            if (newDesign.getLocationType() != null) {
                design.setName(newDesign.getName());
            }
            if (!newDesign.getCategories().isEmpty()) {
                design.setCategories(newDesign.getCategories().stream().map(this::mapCatgory).toList());
            }
            repo.save(design);
            return true;
        }
        return false;
    }
    public void duplicateDesign(String id) {
        List<Design> userDesigns = userService.getUserDesigns(authenticatedUserEmail());
        userDesigns.stream().filter(design -> Objects.equals(design.getId(), id)).toList().stream().findFirst().ifPresent(design -> {
            Design copy_design = Design.builder()
                    .name(design.getName())
                    .categories(design.getCategories())
                    .description(design.getDescription())
                    .locationType(design.getLocationType())
                    .state(design.getState())
                    .created_at(LocalDateTime.now())
                    .build();
            //String name= design.getName().contains("_copy")?design.getName().replaceAll("_copy.*","_copy "+copy_design.getDuplicate()):design.getName().concat("_copy "+copy_design.getDuplicate()+1) ;
            copy_design.setName(design.getName().concat("_copy_"+(design.getDuplicate()+1)));
            copy_design.setContent(design.getContent());
            design.setDuplicate(design.getDuplicate()+1);
            repo.save(copy_design);
            repo.save(design);
            userService.addDesign(copy_design.getName());
        });
    }
    public String authenticatedUserEmail() {
        UserToUserDetails user = userService.getCurrentUser();
        return user.getEmail();
    }
    public void deleteDesign(String id) {
        List<Design> userDesigns = userService.getUserDesigns(authenticatedUserEmail());
        userDesigns.stream().filter(design -> Objects.equals(design.getId(), id)).toList().stream().findFirst().ifPresent(repo::delete);
    }


}
