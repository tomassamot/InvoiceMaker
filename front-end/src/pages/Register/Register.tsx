import {useState, useEffect} from "react";
import {Link, useNavigate} from "react-router-dom";
import "./Register.css";
import {fetchCountries} from "components/CountryFetcher";
import UsernameField from "components/UsernameField";
import PasswordField from "components/PasswordField";
import ConfirmPasswordField from "components/ConfirmPasswordField";
import IsProviderCheckbox from "components/IsProviderCheckbox";
import IsPayingVATCheckbox from "components/IsPayingVATCheckbox";
import LocationDropdown from "components/LocationDropdown";

export interface Country{
  name: string;
  region: string;
  vat: number;
}
interface Account{
  username: string
  password: string
  confirmPassword: string 
  locationName: string
  locationRegion: string
  locationVAT: number
  isProvider: boolean
  isPayingVAT: boolean
}
export default function Register() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [selectedLocationIndex, setSelectedLocationIndex] = useState(0);
  const [allLocations, setAllLocations] = useState<Country[]>();
  const [isProvider, setIsProvider]=useState(false);
  const [isPayingVAT, setIsPayingVAT]=useState(false);

  const [validator, setValidator] = useState("");

  const navigate = useNavigate();

  useEffect(()=>{
    handleFetchData();
  }, []);
  const handleFetchData =async ()=>{
    setAllLocations(await fetchCountries());
  }

  /*const validateFields = () =>{
    let fieldsAreValid = true;

    setUsernameValidator("");
    setPasswordValidator("");
    setConfirmPasswordValidator("");

    if(username === ""){
      setUsernameValidator("Username can't be empty")
      fieldsAreValid = false;
    }
    if(password === ""){
      setPasswordValidator("Password can't be empty");
      fieldsAreValid = false;
    }
    if(confirmPassword !== password){
      setConfirmPasswordValidator("Doesn't match with password");
    }
    if(confirmPassword === ""){
      setConfirmPasswordValidator("Password confirmation can't be empty");
      fieldsAreValid = false;
    }

    return fieldsAreValid;
  }*/

  const handleSubmit = (event: { preventDefault: () => void }) =>{
    event.preventDefault();

      let pickedLocation : Country = allLocations !== undefined ? allLocations[selectedLocationIndex] : {name: "", region: "", vat: 0};

      let account : Account = {username: username, password: password, confirmPassword: confirmPassword, locationName: pickedLocation.name, locationRegion: pickedLocation.region, locationVAT: pickedLocation.vat, isProvider: isProvider, isPayingVAT: isPayingVAT}

      fetch("https://localhost:7231/Account",{
        method: "POST",
        body: JSON.stringify(account),
        headers: {
          "Content-Type": "application/json"
        }
      })
      .then(async (response)=>{
        if(response.status === 201){
          navigate("/Login");
        }
        else if(response.status === 400){
          response.text()
          .then((data)=>{
            setValidator(data);
          })
          /*response.json()
          .then((data)=>{
            setUsernameValidator(data);
          })*/
        }
      })
      .catch((error)=>console.error(error));
  }
  const handleUsernameChange = (newUsername: React.ChangeEvent<HTMLInputElement>)=>{
    setUsername(newUsername.target.value);
  }
  const handlePasswordChange = (newPassword : React.ChangeEvent<HTMLInputElement>)=>{
    setPassword(newPassword.target.value);
  }
  const handleConfirmPasswordChange = (newConfirmPassword : React.ChangeEvent<HTMLInputElement>)=>{
    setConfirmPassword(newConfirmPassword.target.value);
  }
  const handleIsProviderChange = ()=>{
    setIsProvider(!isProvider);
  }
  const handleIsPayingVATChange = ()=>{
    setIsPayingVAT(!isPayingVAT);
  }
  const handleLocationChange = (newLocationIndex : number)=>{
    setSelectedLocationIndex(newLocationIndex);
  }
  return (
    <>
    <h1>REGISTER</h1>
    <form onSubmit={handleSubmit}>
      <div className="wrapper">
        <div className="column-wrapper" id="not-location">
          {
            validator !== "" ?
            (<label style={{color: "red"}}>* {validator}</label>)
            : ""
          }
          <UsernameField
            username={username}
            handleUsernameChange={handleUsernameChange}
          />
          <PasswordField
            password={password}
            handlePasswordChange={handlePasswordChange}
          />
          <ConfirmPasswordField
            confirmPassword={confirmPassword}
            handleConfirmPasswordChange={handleConfirmPasswordChange}
          />
          <IsProviderCheckbox
            isProvider={isProvider}
            handleIsProviderChange={handleIsProviderChange}
          />
          <IsPayingVATCheckbox
            isPayingVAT={isPayingVAT}
            handleIsPayingVATChange={handleIsPayingVATChange}
          />
          <input type={"submit"} value={"Register"}/>
        </div>
        <div className="column-wrapper" id="location">
          {
            allLocations !== undefined ?
            <LocationDropdown
              selectedLocationIndex={selectedLocationIndex}
              allLocations={allLocations}
              handleLocationChange={handleLocationChange}
            />
            : ""
          }
        </div>
      </div>
    </form>
    </>
    );
  }