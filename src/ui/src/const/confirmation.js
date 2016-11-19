import { Enum } from "enumify";

class ConfirmationState extends Enum { }

ConfirmationState.initEnum(["PENDING", "SUCCESS", "FAILURE"]);

export default ConfirmationState; 