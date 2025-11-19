export class Street {
    constructor(StreetName, StreetNumber, PostalCode, City, Country, Province) {
        this.StreetName = StreetName;
        this.StreetNumber = StreetNumber;
        this.PostalCode = PostalCode;
        this.City = City;
        this.Country = Country;
        this.Province = Province;
    }
}

export class Workshop{
    constructor(Name, Email, Password, ConfirmedPassword, PhoneNumber, NIP, Street) {
        this.Name = Name;
        this.Email = Email;
        this.Password = Password;
        this.ConfirmedPassword = ConfirmedPassword;
        this.PhoneNumber = PhoneNumber;
        this.NIP = NIP;
        this.Street = Street;
    }
}