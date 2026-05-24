# WPF EventAggregator

![NET](https://img.shields.io/badge/NET-10-green.svg)
![License](https://img.shields.io/badge/License-MIT-blue.svg)
![VS2026](https://img.shields.io/badge/Visual%20Studio-2026-white.svg)
![Version](https://img.shields.io/badge/Version-1.0.2026.7-yellow.svg)

# Projekt
Das Beispiel soll das Zusammenwirken der Kommunikation zwischen Klassen und UserControl in einer WPF Anwendung zeigen.
<br/>
<img src="WindowsMain.png" style="width:650px;"/>
<br/>
Es verwendet das EventAggregator Pattern, um die Kommunikation zwischen den Komponenten zu ermöglichen. Es gibt drei Klassen, die das IEventAggregator Interface implementieren: MainWindow, ContentLinksUC und ContentRechtsUC. Jede Klasse kann Nachrichten senden und empfangen, ohne direkt voneinander abhängig zu sein.\
<br/>
<img src="EventAggregator.jpg" style="width:650px;"/>

````csharp
App.EventAgg.Subscribe<StatusEvent>(async (evt, ct) => this.OnUpdateStatusBar(evt));
````
Der Übergebene Typ z.B. `StatusEvent' in Subscribe gibt an, welche Art von Nachrichten die Klasse empfangen möchte. In diesem Beispiel abonnieren die Klassen auf Nachrichten vom Typ "StatusEvent". Wenn eine Nachricht dieses Typs gesendet wird, wird die Methode "OnUpdateStatusBar" aufgerufen, um die Statusleiste zu aktualisieren.

Von den beiden UserControls ContentLinksUC und ContentRechtsUC gibt es jeweils einen Button, der eine Nachricht sendet, wenn er geklickt wird. Die Nachricht enthält Informationen darüber, von welchem UserControl sie gesendet wurde. Wenn die Nachricht empfangen wird, wird die Statusleiste in MainWindow aktualisiert, um anzuzeigen, welche Nachricht empfangen wurde.
<img src="Demo_A.png" style="width:650px;"/>


# Möglichkeiten
Über den EventAggregator können Nachrichten mit einem bestimmten Typ gesendet werden, und alle Klassen, die diesen Typ abonnieren, erhalten die Nachricht. In diesem Beispiel gibt es zwei Nachrichten: "Nachricht von Links" und "Nachricht von Rechts". Wenn eine Nachricht gesendet wird, wird sie an alle Abonnenten weitergeleitet, die auf diesen Nachrichtentyp hören.

Es ist aber auch möglich, Komplexe Objekte als Nachrichten zu senden, um mehr Informationen zu übermitteln. In diesem Beispiel werden einfache Strings verwendet, aber es könnte auch ein benutzerdefiniertes Objekt mit mehreren Eigenschaften sein.

![Version](https://img.shields.io/badge/Version-1.0.2026.2-yellow.svg)
- Migration auf NET 10
