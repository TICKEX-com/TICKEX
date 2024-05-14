package com.SeatMap.Designer.Controllers;

import com.SeatMap.Designer.Enums.LocationType;
import com.SeatMap.Designer.Models.Design;
import com.SeatMap.Designer.Services.DesignService;
import com.SeatMap.Designer.Services.UserService;
import com.SeatMap.Designer.dtos.DesignRequest;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.*;

import java.util.Arrays;
import java.util.List;
import java.util.Optional;
@CrossOrigin(origins = {"http://localhost:3000"},allowCredentials = "true")
@RestController
@RequestMapping("/api/design")
public class DesignController {
    private final DesignService designService;
    @Autowired
    public DesignController(DesignService designService  ) {
        this.designService = designService;
    }
    @PostMapping
    public ResponseEntity<String> createDesign(@RequestBody @Valid DesignRequest designRequest) {
        try {
            designService.addDesign(designRequest);
            return ResponseEntity.status(HttpStatus.CREATED).body("Success Insert");
        }
        catch (Exception e) {
            System.out.println(e.getMessage());
            return ResponseEntity.status(HttpStatus.CONFLICT).body("Insert Failed");
        }
    }
    @PostMapping("/duplicate/{id}")
    public ResponseEntity<String> duplicateDesignById( @PathVariable String id) {
        try {
            designService.duplicateDesign(id);
            return ResponseEntity.status(HttpStatus.CREATED).body("Success Duplication");
        }
        catch (Exception e) {
            return ResponseEntity.status(HttpStatus.CONFLICT).body(e.getMessage());
        }
    }
    @DeleteMapping("/{id}")
    public ResponseEntity<String> DeleteDesignById(@PathVariable String id) {
        try {
            designService.deleteDesign(id);
            return ResponseEntity.status(HttpStatus.OK).body("Success Deletion");
        }
        catch (Exception e) {
            return ResponseEntity.status(HttpStatus.CONFLICT).body("Deletion Failed");
        }
    }
    @GetMapping("/{id}")
    public ResponseEntity<Design>  getDesignById(@PathVariable String id){
        Optional<Design> design = designService.findDesignById(id);
        return ResponseEntity.status(HttpStatus.OK).body(design.orElse(null)) ;
    }
    @GetMapping("/{name}")
    public ResponseEntity<Design>  getDesignByName(@PathVariable String name){
        Optional<Design> design = designService.findDesignByName(name);
        return ResponseEntity.status(HttpStatus.OK).body(design.orElse(null)) ;
    }
    @PutMapping("/specs/{id}")
    public ResponseEntity<String> updateDesignSpecs(@PathVariable String id,@RequestBody DesignRequest newDesign){
        if(designService.updateSpecsById(id,newDesign)){
            return ResponseEntity.status(HttpStatus.OK).body("Success Update");
        }
        else  {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Update Failed");
        }
    }
    @PutMapping("/content/{id}")
    public ResponseEntity<String> updateDesignContent(@PathVariable String id,@RequestBody String newContent){
        if(designService.updateContentById(id,newContent)){
            return ResponseEntity.status(HttpStatus.OK).body("Success Update");
        }
        else  {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Update Failed");
        }
    }
    @GetMapping("/location-types")
    public ResponseEntity<List<String>> getLocationTypes(){
        LocationType[] locations = LocationType.values();
        return ResponseEntity.status(HttpStatus.OK).body(Arrays.stream(locations).map(Object::toString).toList());
    }
}
