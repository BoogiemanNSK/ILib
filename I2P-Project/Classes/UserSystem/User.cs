namespace I2P_Project.Classes.UserSystem
{

    // TODO Make integration with DB and getters by ID
    abstract class User
    {
      
        public int ID { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public int PhoneNumber { get; set; }

        public int LibraryCardNumber { get; set; }

    }

}
