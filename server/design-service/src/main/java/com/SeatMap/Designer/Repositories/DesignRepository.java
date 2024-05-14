package com.SeatMap.Designer.Repositories;

import com.SeatMap.Designer.Models.Design;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface DesignRepository extends MongoRepository<Design, String> {
    Optional<Design> findByName(String name);
}
