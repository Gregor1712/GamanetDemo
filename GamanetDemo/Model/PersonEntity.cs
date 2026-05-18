using Gamanet.Common;

namespace GamanetDemo.Model;

internal class PersonEntity : PropertyChangedBase
{
    private string _name = string.Empty;
    private string _country = string.Empty;
    private string _phone = string.Empty;
    private string _email = string.Empty;

    public string Name
    {
        get => _name;
        set {
            if (_name.Equals(value) == false)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }

    public string Country
    {
        get => _country;
        set {
            if (_country.Equals(value) == false)
            {
                _country = value;
                OnPropertyChanged();
            } 
        }
    }

    public string Phone
    {
        get => _phone;
        set {
            if (_phone.Equals(value) == false)
            {
                _phone = value;
                OnPropertyChanged();
            }
        }
    }

    public string Email
    {
        get => _email;
        set {
            if (_email.Equals(value) == false)
            {
                _email = value;
                OnPropertyChanged();
            }
        }
    }
}