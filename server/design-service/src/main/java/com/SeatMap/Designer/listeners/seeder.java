package com.SeatMap.Designer.listeners;

import com.SeatMap.Designer.Models.User;
import com.SeatMap.Designer.Repositories.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.event.ContextRefreshedEvent;
import org.springframework.context.event.EventListener;
import org.springframework.stereotype.Component;

@Component
public class seeder {
    @Autowired
    UserRepository userRepository;
    @EventListener
    public void seed(ContextRefreshedEvent event) {
        seedUsersTable();
       // seedCategoryTable();
        //seedSectionsTable();
    }

    private void seedUsersTable() {
        if (userRepository.count() == 0) {
            User user1 = new User();
            userRepository.save(user1);
        }
        System.out.println(userRepository.count());
    }
}
