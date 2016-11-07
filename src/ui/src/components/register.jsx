import React from "react";
import { browserHistory } from "react-router";

import Form from "react-bootstrap/lib/Form";
import FormControl from "react-bootstrap/lib/FormControl";
import FormGroup from "react-bootstrap/lib/FormGroup";
import ControlLabel from "react-bootstrap/lib/ControlLabel";
import Button from "react-bootstrap/lib/Button";
import Panel from "react-bootstrap/lib/Panel";
import Col from "react-bootstrap/lib/Col";

import RegistrationOk from "./registration_ok";
import RegistrationFailure from "./registration_failure";

const registration_state_ok = "ok";
const registration_state_failure = "failure";
const registration_state_registering = "registering";

export default class RegisterForm extends React.Component {

	constructor(props) {
		super(props);
		this.state =
			{
				email: "",
				password: "",
				registration_state: ""
			};

		this.onEmailChange = this.onEmailChange.bind(this);
		this.onPasswordChange = this.onPasswordChange.bind(this);
		this.onRegisterButtonClicked = this.onRegisterButtonClicked.bind(this);
	}

	onEmailChange(event) {
		this.setState({ email: event.target.value });
	}

	onPasswordChange(event) {
		this.setState({ password: event.target.value });
	}

	onRegisterButtonClicked(e) {
		e.preventDefault();

		this.setState({ registration_state: registration_state_registering });

		var data = {
			email: this.state.email,
			password: this.state.password
		};

		var json = JSON.stringify(data);

		var options = {
			method: "post",
			headers: {
				"Accept": "application/json",
				"Content-Type": "application/json"
			},
			body: json
		};

		var url = "http://localhost:8000/api/users";

		fetch(url, options)
			.then(response => {
				if (response.status == 200) {
					this.setState({ registration_state: registration_state_ok });
				}
				else {
					this.setState({ registration_state: registration_state_failure });
				}
			})
			.catch(err => {
				this.setState({ registration_state: registration_state_failure });
			});
	}

	render() {
		return (
			<Panel header="Создание нового пользователя">
				<Form>
					<FormGroup>
						<Col sm={2} componentClass={ControlLabel}>Email</Col>
						<Col sm={10}>
							<FormControl
								type="email"
								placeholder="Введите почтовый адрес"
								value={this.state.email}
								onChange={this.onEmailChange} />
						</Col>
					</FormGroup>

					<FormGroup>
						<Col sm={2} componentClass={ControlLabel}>Пароль</Col>
						<Col sm={10}>
							<FormControl
								type="password"
								placeholder="Введите пароль"
								value={this.state.password}
								onChange={this.onPasswordChange} />
						</Col>
					</FormGroup>

					<FormGroup>
						<Col smOffset={2} sm={10}>
							<Button
								type="submit"
								disabled={this.state.registration_state === registration_state_registering}
								onClick={this.onRegisterButtonClicked}>Создать</Button>
						</Col>
					</FormGroup>

					{this.state.registration_state === registration_state_ok ? <RegistrationOk /> : null}
					{this.state.registration_state === registration_state_failure ? <RegistrationFailure /> : null}
				</Form>
			</Panel>
		);
	}
}