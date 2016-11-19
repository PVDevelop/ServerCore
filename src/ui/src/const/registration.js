import { Enum } from "enumify";

class RegistrationState extends Enum { }

RegistrationState.initEnum(["NONE", "PENDING", "SUCCESS", "FAILURE"]);

export default RegistrationState; 