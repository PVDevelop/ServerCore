import { Enum } from "enumify";

class RegistrationState extends Enum { }

RegistrationState.initEnum(["NONE", "SUCCESS", "PENDING", "FAILURE"]);

export default RegistrationState; 