using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities
{
    /**
     * This class will contain all the needed data to initialize
     * a worker in the crawler, such as the list of categories defined
     * by the user, the constraints for the filter.
     */
    public class Initializer
    {
        //categoryList contains the categories defined by the user.
        private List<Category> categoryList;

        //constraintsOptions contains the constraints defined by the user. 
        private Constraints constraintsOptions;

        public Initializer(Constraints _constraints, List<Category> _categories)
        {
            constraintsOptions = _constraints;
            categoryList = _categories;
        }

        /**
         * This method returns the categoryList
         */
        public List<Category> getCategoryList()
        {
            return categoryList;
        }

        /**
         * This method returns the Constraints.
         */
        public Constraints getContraints()
        {
            return constraintsOptions;
        }
    }
}
