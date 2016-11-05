import React from "react";
import { browserHistory } from "react-router";

export default class RegisterForm extends React.Component {
	constructor(props) {
		super(props);
		this.state =
			{
				email: '',
				password: '',
				showOk: false
			};

		this.onEmailChange = this.onEmailChange.bind(this);
		this.onPasswordChange = this.onPasswordChange.bind(this);
		this.onRegisterButtonClicked = this.onRegisterButtonClicked.bind(this);
		this.onRegistrationOk = this.onRegistrationOk.bind(this);
		this.onOkButtonClicked = this.onOkButtonClicked.bind(this);
	}

	onEmailChange(event) {
		this.setState({ email: event.target.value });
	}

	onPasswordChange(event) {
		this.setState({ password: event.target.value });
	}

	onRegistrationOk() {
		this.setState({ showOk: true });
	}

	onRegisterButtonClicked(e) {
		e.preventDefault();

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
					this.onRegistrationOk();
				}
				else {
					alert(response.status);
				}
			})
			.catch(err => alert(err));
	}

	onOkButtonClicked(e)
	{
		browserHistory.push('/');
	}

	render() {
		return (
			<form>
				<div className="form-group">
					<label htmlFor="inputEmail">Email</label>
					<input
						id="inputEmail" 
						type="email"
						className="form-control"
						placeholder="Введите почтовый адрес"
						value={this.state.email}
						onChange={this.onEmailChange} />
				</div>

				<div className="form-group">
					<label htmlFor="inputPassword">Пароль</label>
					<input
						id="inputPassword"
						type="password"
						className="form-control"
						placeholder="Введите пароль"
						value={this.state.password}
						onChange={this.onPasswordChange} />
				</div>

				<div className="form-group">
					<button
						type="submit"
						className="btn btn-default"
						data-toggle="modal" 
						data-target="#modalOk"
						onClick={this.onRegisterButtonClicked}>
						Создать
					</button>
				</div>

				<div className="modal fade" id="modalOk" tabIndex="-1" role="dialog">
					<div className="modal-dialog" role="document">
						<div className="modal-content">
							<div className="modal-header">
								<h4 className="modal-title">Пользователь зарегистрирован.</h4>
							</div>
							<div className="modal-body">
								<p>Подтверждение о регистрации отправлено на указанный почтовый адрес.</p>
							</div>
							<div className="modal-footer">
								<button
									className="btn btn-default" 
									onClick = {this.onOkButtonClicked}>
									OK
								</button>
							</div>
						</div>
					</div>
				</div>
			</form>
		);
	}
}