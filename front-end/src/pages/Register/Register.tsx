import {useState, useEffect} from "react";
import {Link} from "react-router-dom";
import "./Register.css";
import UsernameField from "components/UsernameField";
import PasswordField from "components/PasswordField";
import ConfirmPasswordField from "components/ConfirmPasswordField";
import IsProviderCheckbox from "components/IsProviderCheckbox";
import IsPayingVATCheckbox from "components/IsPayingVATCheckbox";
import LocationDropdown from "components/LocationDropdown";

export interface Location{
  name: string;
  region: string;
  vat: number;
}
export default function Register() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [selectedLocationIndex, setSelectedLocationIndex] = useState(0);
  const [allLocations, setAllLocations] = useState<Location[]>();
  const [isProvider, setIsProvider]=useState(false);
  const [isPayingVAT, setIsPayingVAT]=useState(false);

  const [usernameValidator, setUsernameValidator] = useState("");
  const [passwordValidator, setPasswordValidator] = useState("");
  const [confirmPasswordValidator, setConfirmPasswordValidator] = useState("");

  let locationIndex=-1;
  useEffect(()=>{
    fetch("https://localhost:7231/Country",{
      method: "GET",
      headers: {
        
      }
    })
    .then((response)=>
    response.json()
    .then((data)=> setAllLocations(data))
    .then(()=>{
      console.log("cycle start");
      if(allLocations !== undefined){
        for(let i = 0;i<allLocations.length;i++){
          console.log(i+". "+allLocations[i]);
        }
      }
    })
    .catch((error)=>console.error(error))
    )
  }, [])

  const validateFields = () =>{
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
  }
  const handleSubmit = (event: { preventDefault: () => void }) =>{
    event.preventDefault();
    let fieldsAreValid = validateFields();

    if(fieldsAreValid){
      // fetch POST
      if(true /*fetch response is created*/){
        console.log("register fetch was good")
        // give feedback
        // navigate("/login");
      }
      else{
        console.log("register fetch was bad");
        // set username validator to response message
      } 
    }
    else{
    console.log("register was bad");
    }
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
    <form onSubmit={handleSubmit}>
      <div className="wrapper">
        <div className="column-wrapper" id="not-location">
          <UsernameField
            usernameValidator={usernameValidator}
            username={username}
            handleUsernameChange={handleUsernameChange}
          />
          <PasswordField
            passwordValidator={passwordValidator}
            password={password}
            handlePasswordChange={handlePasswordChange}
          />
          <ConfirmPasswordField
            confirmPasswordValidator={confirmPasswordValidator}
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