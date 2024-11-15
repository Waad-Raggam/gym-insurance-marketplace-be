using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Entity;

// DTO (Data Transfer Object):
// Role: Used to transfer data between layers of the application, 
// typically between the controller and service.

namespace src.DTO
{
    public class CategoryDTO
    {
        /// <summary>
        /// Data Transfer Object (DTO)
        /// This DTO is designed to encapsulate all necessary information.
        /// transfer data between different parts of an application,
        /// such as between services, and repositories.
        /// Mapper used the class down there.
        /// Contains category details such as Name.
        /// This DTO is designed to encapsulate all necessary information for category creation.
        /// </summary>

        // create category
        // in create i don't need to send the Gemstones 
        public class CategoryCreateDto
        {
            public string CategoryName { get; set; }

        }

        // read data = get data 
        public class CategoryReadDto
        {
            public Guid CategoryId { get; set; }
            public string CategoryName { get; set; }

        }

        // update
        public class CategoryUpdateDto
        {
            public string CategoryName { get; set; }

        }

        

    } // end class
} // end namespace